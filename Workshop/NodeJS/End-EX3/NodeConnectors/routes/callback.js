var express = require("express");
var router = express.Router();
var mongojs = require("mongojs");
var db = mongojs("billslist", ["subscriptions"]);

/* GET /callback */
router.get("/", function(req, res, next) {
    //ensure the parameters were returned
    var error = req.query.error;
    var state = req.query.state;
    if (error !== undefined) {
        //something went wrong with consent flow
        res.redirect("/error?error=" + error);
    }
    else {
        var group = req.query.group_name;
        var webhook = req.query.webhook_url;

        //initialize the subscription
        var subscription = {
            GroupName: group,
            WebHookUri: webhook
        }

        //save the subscription and the redirect to state
        db.subscriptions.save(subscription, function(err, sub) {
            if( err || !sub ) {
                //something went wrong...redirect to error
                res.redirect("/error?error=subscription not saved");
            }
            else {
                //redirect to original location (state)
                res.redirect(state);
            }
        });
    }
});

module.exports = router;
