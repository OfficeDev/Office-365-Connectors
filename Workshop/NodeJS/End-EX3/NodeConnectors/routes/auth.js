var express = require("express");
var router = express.Router();
var authHelper = require("../authHelper.js");

/* POST to root of route */
router.post("/", function(req, res, next) {
    //TODO: should validate the id_token here
    var segments = req.body.id_token.split(".");
    var payload = JSON.parse(base64Decode(segments[1]));

    //save the preferred user as cookie
    res.cookie(authHelper.TOKEN_CACHE_KEY, payload.preferred_username);
    res.redirect(req.body.state);
});

function base64Decode(str) {
    str += Array(5 - str.length % 4).join("=");
    str = str.replace(/\-/g, "+").replace(/_/g, "/");
    return new Buffer(str, "base64").toString();
};

module.exports = router;
