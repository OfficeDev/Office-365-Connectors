var express = require("express");
var router = express.Router();
var authHelper = require("../authHelper.js");
var mongojs = require("mongojs");
var db = mongojs("billslist", ["items", "subscriptions"]);
var multiparty = require("multiparty");
var fs = require("fs");
var btoa = require("btoa");
var https = require("https");
var im = require("imagemagick");

//list of categories on the site
var categories = ["antiques", "appliances", "arts & crafts",
            "atvs, utvs, snowmobiles", "auto parts", "baby & kid stuff", "barter", "bicycles", "boats", "books & magazines",
            "business/commercial", "cars & trucks", "cds, dvds, vhs", "cell phones", "clothing & accessories",
            "collectables", "computers", "electronics", "farm & garden", "free stuff", "furniture" ,"garage & moving sales",
            "general for sale", "health & beauty", "heavy equipment", "household items", "jewelry", "materials",
            "motorcycles/scooters", "musical instruments", "photo/video", "rvs", "sporting goods", "tickets",
            "tools", "toys & games", "video gaming", "wanted" ];

/* GET /items */
router.get("/", function(req, res, next) {
    if (authHelper.ensureAuth(req, res)) {
        //query items and display view
        db.items.find({ }, function(err, items) {
            res.render("items/index", { title: "Items", items: items });
        });
    }
});

/* GET /items */
router.get("/category", function(req, res, next) {
    if (authHelper.ensureAuth(req, res)) {
        //query items and display view
        db.items.find({ Category: req.query.c }, function(err, items) {
            res.render("items/category", { title: "Items", items: items, category: req.query.c });
        });
    }
});

/* GET /items/my */
router.get("/my", function(req, res, next) {
    if (authHelper.ensureAuth(req, res)) {
        //query items and display view
        db.items.find({ Owner: req.cookies.TOKEN_CACHE_KEY }, function(err, items) {
            res.render("items/my", { title: "Items", items: items });
        });
    }
});

/* GET /items/create */
router.get("/create", function(req, res, next) {
    if (authHelper.ensureAuth(req, res)) {
        res.render("items/create", { title: "Create", categories: categories });
    }
});

/* POST /items/create */
router.post("/create", function(req, res, next) {
    if (authHelper.ensureAuth(req, res)) {
        var form = new multiparty.Form();
        form.parse(req, function(err, fields, files) {
            var newItem = {
                Id: getRandomId(8),
                Owner: req.cookies.TOKEN_CACHE_KEY,
                Title: fields.txtTitle[0],
                Price: parseFloat(fields.txtPrice[0]),
                Location: fields.txtLocation[0],
                Category: fields.cboCategory[0],
                Body: fields.txtBody[0]
            };

            //get base64 image from fileImage
            fs.readFile(files.fileImage[0].path, function (err, data) {
                newItem.Image = btoa(data);

                //resize the image to get thumbnail
                im.resize({
                    srcData: fs.readFileSync(files.fileImage[0].path, "binary"),
                    width:  50
                    }, function(err, stdout, stderr){
                        if (err) throw err;

                        //convert back to base-64
                        var thumbnail = new Buffer(stdout, "binary").toString("base64");
                        
                        //create the items
                        db.items.save(newItem, function(err, saved) {
                            if( err || !saved ) {
                                //something went wrong...redirect to error
                                res.redirect("/error?error=item not saved");
                            }
                            else {
                                //process all subscriptions
                                db.subscriptions.find({}, function(e, subs) {
                                    var payload = {
                                        "summary": "A new listing was posted to BillsList",
                                        "sections": [
                                            {
                                                "activityTitle": "New BillsList listing",
                                                "activitySubtitle": newItem.Title,
                                                "activityImage": "data:image/png;base64," + thumbnail,
                                                "facts": [
                                                    {
                                                        "name": "Category",
                                                        "value": newItem.Category
                                                    },
                                                    {
                                                        "name": "Price",
                                                        "value": "$" + newItem.Price
                                                    },
                                                    {
                                                        "name": "Listed by",
                                                        "value": newItem.Owner
                                                    }
                                                ]
                                            }],
                                            "potentialAction": [
                                                {
                                                    "@context": "http://schema.org",
                                                    "@type": "ViewAction",
                                                    "name": "View in BillsList",
                                                    "target": [
                                                        "https://localhost:44300/items/detail/" + newItem.Id
                                                    ]
                                                }
                                            ]};

                                    subs.forEach(function(sub) {
                                        var url = sub.WebHookUri.substring(8);
                                        var host = url.substring(0, url.indexOf("/"));
                                        var path = url.substring(url.indexOf("/"));

                                        //just let the post go...we use a promise but don't wait for response
                                        postJson(host, path, JSON.stringify(payload)).then(function(result) {
                                            //Do Nothing
                                        }, function(err) {
                                            console.log(err);
                                        });
                                    });
                                });

                                //redirect
                                res.redirect("/items/my");
                            }
                        });
                    });
            });
        });
    }
});

/* GET /items/delete/:id */
router.get("/delete/:id", function(req, res, next) {
    //TODO: deleting from a GET is lazy
    if (authHelper.ensureAuth(req, res)) {
        db.items.remove({ Id: req.params.id }, function(err, items) {
            res.redirect("/items/my");
        });
    }
});

/* GET /items/detail/:id */
router.get("/detail/:id", function(req, res, next) {
    if (authHelper.ensureAuth(req, res)) {
        //find the item by id
        db.items.find({ Id: req.params.id }, function(err, items) {
            res.render("items/detail", { title: items[0].Title, item: items[0] });
        });
    }
});

//generates a random id for items
function getRandomId(len) {
    var val = "";
    var chars = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];
    for (var i = 0; i < len; i++) {
        var index = Math.floor(Math.random() * chars.length);
        val += chars[index];
    }
    return val;
};

//performs a generic http POST and returns JSON
function postJson(host, path, payload) {
    //return promise
    return new Promise((resolve, reject) => {
        var options = {
            host: host, 
            path: path, 
            method: "POST",
            headers: { 
                "Content-Type": "application/json",
                "Content-Length": Buffer.byteLength(payload, "utf8")
            }
        };
        
        var reqPost = https.request(options, function(res) {
            var body = "";
            res.on("data", function(d) {
                body += d;
            });
            res.on("end", function() {
                resolve(JSON.parse(body));
            });
            res.on("error", function(e) {
                reject(e);
            });
        });
        
        //write the data
        reqPost.write(payload);
        reqPost.end();
    });
};

module.exports = router;
