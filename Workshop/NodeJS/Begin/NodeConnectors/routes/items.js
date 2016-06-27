var express = require("express");
var router = express.Router();
var authHelper = require("../authHelper.js");
var mongojs = require("mongojs");
var db = mongojs("billslist", ["items"]);
var multiparty = require("multiparty");
var fs = require("fs");
var btoa = require("btoa");


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

                //create the items
                db.items.save(newItem, function(err, saved) {
                    if( err || !saved ) {
                        //TODO???
                        console.log("Item not saved");
                    }
                    else {
                        //redirect
                        res.redirect("/items/my");
                    }
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

function getRandomId(len) {
    var val = "";
    var chars = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];
    for (var i = 0; i < len; i++) {
        var index = Math.floor(Math.random() * chars.length);
        val += chars[index];
    }
    return val;
};

module.exports = router;
