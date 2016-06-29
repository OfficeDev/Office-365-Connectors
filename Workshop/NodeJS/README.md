<a name="HOLTop" />
# Building Office 365 Connectors - NodeJS #

---

<a name="Overview" />
## Overview ##

Office 365 now enables developers to build a powerful new generation of **conversation**-driven applications. Office 365 Connectors are a great way to get useful information and content into your Office 365 Group. Any user can connect their group to services like Trello, Bing News, Twitter, etc., and get notified of the group's activity in that service and allow users to have conversations, collaborate, and take action on it. From tracking a team's progress in Trello, to following important hashtags in Twitter, Office 365 Connectors make it easier for an Office 365 group to stay in sync and get more done. Office 365 Connectors also provides a compelling extensibility solution for developers which we will explore in this Exercise.

In this Lab, you will explore development with Office 365 Connectors, including how to integrate them into existing web applications.

<a name="Objectives" />
### Objectives ###
In this module, you'll see how to:

- Manually register an Office 365 Connector and use a webhook to send data into Office 365.
- Connect an existing web application to Office 365 via Office 365 Connectors, including the addition of "Connect to Office 365" buttons, handling callbacks from Office 365, and sending data into Office 365 via webhooks.
- Modify the web application to support complex Connector subscriptions.

<a name="Prerequisites"></a>
### Prerequisites ###

The following is required to complete this module:

- [Code Editor such as Visual Studio Code][1]
- [Office 365 Tenant][2]
- [Any Web Request Composer][2]

[1]: https://code.visualstudio.com/
[2]: https://portal.office.com/Signup?OfferId=B07A1127-DE83-4a6d-9F85-2C104BDAE8B4
[3]: https://www.hurl.it

> **Note:** Visual Studio Code is a free open-source platform with rich Node/JavaScript debugging capabilities.

<a name="Exercises" />
## Exercises ##
This module includes the following exercises:

1. [Getting Started with Office 365 Connectors](#Exercise1)
2. [Leveraging Webhooks with Office 365 Connectors](#Exercise2)
3. [Integrating "Connect to Office 365" into Existing Applications](#Exercise3)
4. [Developing Filtered Connector Subscriptions](#Exercise4)

Estimated time to complete this module: **60 minutes**

<a name="Exercise1"></a>
### Exercise 1: Getting Started with Office 365 Connectors ###

In this Exercise, you will explore Office 365 Groups and some of the existing Office 365 Connectors that are availablen in the Connector catalog of Office 365.

1. Open a browser and navigate to [https://outlook.office.com](https://outlook.office.com "https://outlook.office.com") and sign-in with the Office 365 credentials that were provided to you.

2. Once you are signed into OWA, locate the Office 365 Groups you are a member of in the lower left navigation.

	![Groups in OWA](Images/Mod4_Groups.png?raw=true "Groups in OWA")

	 _Groups in OWA_

3. Create your own unique group by clicking the **+** (plus) sign to the right of the **Groups** title in the left navigation.

	![Add Group](Images/Mod4_AddGroup.png?raw=true "Add Group")

	 _Add Group_

4. Provide a **name** and **description** for the new group and click **Create** (optionally add members once the groups has been created).

5. Select **More > Connectors** from the Group's top navigation (if More isn't an option, you might try navigating back to the group using the link in **Step 2**).

	![Connectors in group navigation](Images/Mod4_ConnectorsMenu.png?raw=true "Connectors in group navigation")

	 _Connectors in group navigation_

6. Explore some of the Office 365 Connectors that are available out of the box.

	![OOTB Connectors](Images/Mod4_Connectors.png?raw=true "OOTB Connectors")

	 _OOTB Connectors_

7. Locate the **Twitter** connector and click **Add**.

8. The Twitter connector requires you to sign-in with a Twitter account. To do this, click on the Log in button.

	![Twitter Connector Log in](Images/Mod4_Twitter1.png?raw=true "Twitter Connector Log in")

	 _Twitter Connector Log in_

9. After authorizing the Office 365 Connector for Twitter, you can select specific **users**/**hashtags** to follow and how **frequently** they show up in the Office 365 Group. Try to follow yourself or a hashtag.

	![Configure Twitter Connector](Images/Mod4_Twitter2.png?raw=true "Configure Twitter Connector")

	 _Configure Twitter Connector_

10. Post a tweet that matches the criteria from the previous step and see it show up in the Office 365 Group (latency might prevent the tweets from showing up in the group immediately).

   ![Twitter post via Office 365 Connector](Images/Mod4_Twitter3.png?raw=true "Twitter post via Office 365 Connector")

	  _Twitter post via Office 365 Connector_

<a name="Exercise2"></a>
### Exercise 2 - Leveraging Webhooks with Office 365 Connectors ###

Hopefully Exercise 1 helped to illustrate the power of Office 365 Connectors, but did little to showcase the unique developer opportunity. In this Exercise, you will explore Office 365 Connector webhooks and how developer can leverage then to send data into Office 365.

1. Navigate to the Office 365 Group you created in the previous Task and select **More > Connectors** from the Group's top navigation.

1. Locate the **Incoming Webhook** Connector and click the **Add** button.

	![Incoming Webhook](Images/Mod4_IncomingWebhook.png?raw=true "Incoming Webhook")

	 _Incoming Webhook_

1. Specify a **name** for the incoming webhook (ex: Build 2016) and click the **Create** button.

1. The confirmation screen will display a **URL** that is the webhook end-point we will use later in this Task.

	![Webhook Confirmation](Images/Mod4_IncomingConfirmation.png?raw=true "Webhook Confirmation")

	_Webhook Confirmation_

1. Open a new browser tab and navigate to [https://www.hurl.it](https://www.hurl.it "https://www.hurl.it"), which is an in-browser web request composer similar to what Fiddler offers.

1. When the page loads, add the following details:
	- **Operation**: **POST**
	- **Destination Address**: **webhook URL** from **Step 4**
	- **Headers**: **Content-Type: application/json**
	- **Body**: **{ "text": "Hello from Build 2016" }**

	![Manual Webhook](Images/Mod4_ManualHook.png?raw=true "Manual Webhook")

    _Manual Webhook_

1. Accept the **Captcha** and click **Launch Request**. You should get a confirmation screen that looks similar to the following.

	![Webhook Manual Confirmation](Images/Mod4_ManualConfirm.png?raw=true "Webhook Manual Confirmation")

	_Webhook Manual Confirmation_

1. If you return to the Office 365 Group, you should be able to locate the message you sent into it via the webhook.

	![Message sent into Group via webhook](Images/Mod4_HookDone.png?raw=true "Message sent into Group via webhook")

	_Message sent into Group via webhook_

1. Although you sent a very simple message into the webhook, Office 365 Connectors support a much more complex message format. You can get more details on the message format by visiting [https://dev.outlook.com/Connectors/GetStarted](https://dev.outlook.com/Connectors/GetStarted "https://dev.outlook.com/Connectors/GetStarted").

<a name="Exercise3"></a>
### Exercise 3 - Integrating "Connect to Office 365" into Existing Applications ###

Exercise 2 had you manually register a webhook for an Office 365 Connector. In this Task, you will modify an existing web application to register webhooks with Office 365 by leveraging a "Connect to Office 365" button. You will capture the webhook details in a custom callback and send messages to Office 365 when new records are created in the application.

This Exercise uses a starter project to serve as the existing application. The application is a Craigslist-style selling site named BillsList (in honor of the great Bill Gates). You are tasked with enhancing BillsList to allow users to subscribe to listing categories and send messages to Office 365 Groups when new listings match the subscription criteria. This Exercise will just get the Connector working without special subscription criteria, which will be added in the next Exercise.

1. Open a command/terminal prompt to the lab's **Begin\NodeConnectors** folder.

1. Perform an **npm install** to pull down all the node modules needed for the sample.

```javascript
        npm install
```

1. The sample uses MongoDB, so go to [https://docs.mongodb.com/manual/installation/](https://docs.mongodb.com/manual/installation/) if you don't have it installed. Not sure if you have it installed? Try running mongod from a command/terminal prompt.

1. The sample also resizes images using an open-source Imagemagick CLI. If you have never used it, you can install it using the command **brew install imagemagick**

1. You also need to use bower to install the appropriate client libraries. Run **bower install bootstrap --save**

```javascript
        bower install bootstrap --save
```

1. Open the NodeConnectors folder in your favorite Node.js code editor (**Visual Studio Code** is used throughout these steps and might be the easiest to follow).

1. Start the MongoDB service by typing **mongod --dbpath data/db**

```javascript
        mongod --dbpath data/db
```

1. Start debugging the Node.js application using the debug tab in Visual Studio Code or type npm start from a command/terminal prompt. The site should be running at [http://localhost:3000](http://localhost:3000)

1. When the application loads, click on **Listings** in the top navigation. This will prompt you to sign-in. Use the **Office 365 account** that was provided to you (it also supports Consumer/MSA accounts like outlook.com, live.com, hotmail.com, etc).

1. The **Listings** view displays all the items listed for sale on the site. As new items are listed, we want Office 365 Groups to be notified via a Connector (in the next Exercise we will take it a step further to establish the Connector for specific categories).

	![Listings](Images/Mod4_Listings.png?raw=true "Listings")

	_Listings_

1. Open a browser and navigate to [https://outlook.office.com](https://outlook.office.com "https://outlook.office.com") and sign-in with the Office 365 account that was provided to you.

1. Once you are signed into Outlook, navigate to the **Connectors Developer Dashboard** [https://outlook.office.com/connectors/publish](https://outlook.office.com/connectors/publish "https://outlook.office.com/connectors/publish"). This is a special page that allows you to register 3rd party connectors and generate "Connect to Office 365" markup.

	![Add Connectors screen](http://i.imgur.com/GKKstbS.png)

	_Connectors Developer Dashboard_

1. Click on the **New Connector** and fill out the following details in the New Connector form in the Connectors Developer Dashboard.

	- **Connector Name**: BillsList
	- **Logo**: (upload the BillsList.png file in the project root)
	- **Short Description**: Any description
	- **Detailed Description**: Any description
	- **Company Website**: Any website (ex: http://www.foo.com)
	- **Landing page for your users**: http://localhost:3000
	- **Redirect URL**: http://localhost:3000/callback

1. Save the Form to generate "Connect to Office 365" button markup in step 2 of the form. Copy this markup for use in subsequent steps.

	![](http://i.imgur.com/1InrXFG.png)

	_New Connector Form_

1. Close the browser to stop debugging and open the **layout.hbs** file located in **views** folder of the project.

1. After the second **navbar-collapse** element (**at line 30**), add following markup as a container for the "Connect to Office 365" button.

 ```html
        <div class="navbar-collapse collapse" style="padding-top: 5px;">
            <div class="nav navbar-nav navbar-right">
  
            </div>
        </div>
 ```

1. Next, paste the "**Connect to Office 365**" button markup generated above into the new div.

 ```html
        <div class="navbar-collapse collapse" style="padding-top: 5px;">
            <div class="nav navbar-nav navbar-right">
                <a href="https://outlook.office.com/connectors/Connect?state=myAppsState&app_id=a786cbb7-f80d-4968-91c0-9df7a96d75f0&callback_url=https://localhost:44300/callback"><img src="https://o365connectors.blob.core.windows.net/images/ConnectToO365Button.png" alt="Connect to Office 365"></img></a>  
            </div>
        </div>
 ```

1. Modify the href with javascript so the **state** URL parameter can be dynamically set to the current page (instead of **myAppsState**)

 ```html
        <div class="nav navbar-nav navbar-right">
            <a href="javascript:window.location='https://outlook.office.com/connectors/Connect?state=' + window.location.href + '&app_id=4b543361-dbc2-4726-a351-c4b43711d6c5&callback_url=http://localhost:3000/callback'"><img src="https://o365connectors.blob.core.windows.net/images/ConnectToO365Button.png" alt="Connect to Office 365"></img></a>
        </div>
 ```

1. You might recall we are passing in a callback location of **http://localhost:3000/callback** to Office 365. However, the **Callback** controller does not yet exist...let's create it. Locate the **routes** folder for the project and add a callback.js file.

1. Inside the **CallbackController** class, add the following code.

 (Code Snippet - _o365-callbackctrl_)

```javascript
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
```

1. The controller looks for information returned from Office 365 and saves it as a subscription. The specific information passed from Office 365 as parameters include:

	- **error**: error details if the connection with Office 365 failed (ex: user rejected the connection)
	- **state**: the state value that was passed in via the "Connect to Office 365" button. In our case, it could include the category the user subscribed to
	- **group_name**: the name of the group the user selected to connect to
	- **webhook_url**: the webhook end-point our application will use to send messages into Office 365

1. Next, you need to update the **app.js** file so it is aware of the new callback route. Open it and add a requires for the callback.js and app.use("/callback", callback) where the other routes are defined.

```javascript
var callback = require("./routes/callback");

//lines removed for brevity

app.use("/callback", callback);

```

1. Almost done, just need to update the **create** activity to send messages to the appropriate webhooks. Open the **items.js** file located in the **routes** folder of the project.

1. Add requires statements for https and imagemagick (used to resize images).

```javascript
        var https = require("https");
        var im = require("imagemagick");
```

1. Add the following method that will perform a generic HTTP POST using the https object

```javascript
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
```

1. Modify the POST **create** activity as seen below. This resizes the item image so it fits in a connector message (as base-64) and sends the message:

 ```javascript
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
 ```

1. Start debugging the Node.js application using the debug tab in Visual Studio Code or type npm start from a command/terminal prompt. The site should be running at [http://localhost:3000](http://localhost:3000). When you click on **Listings** (and sign-in) the "**Connect to Office 365**" button should display in the header.

	![Connect to Office 365 button in header](http://i.imgur.com/6iDZ34T.png)

	_Connect to Office 365 button_

1. Click on the "**Connect to Office 365**" button. You should be redirected to a screen to select a Office 365 Group to connect to.

	![Select group to connect to](http://i.imgur.com/4wj3BKz.png)

	_Select group to connect to_

1. Select an Office 365 Group and click **Allow** to complete establish the connection with Office 365 and return to BillsList.

1. To test the connection, click on **My Listings** and **Create listing** with the **category** you subscribed to. A Connector **Card** for the listing should almost immediately show up in the Office 365 Group.

	![Connect message in group UI](http://i.imgur.com/Jr5u18V.png)

	_Connector card_

<a name="Exercise4"></a>
### Exercise 4 - Filtering Connector Subscriptions ###

The solution so far sends data into Office 365 for every new entry. However, a group might want the Connector to be filtered in sending more information based on specific criteria such as by category.

This Task make modifications to the BillsList solution to filter Connector subscriptions based on specific categories. This will be accomplished by providing a subscription criteria form after the consent callback.

1. Open the callback.js file in the routes folder and add an additional action for POST using the same /callback route.

 ```javascript
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
        
        /* POST /callback */
        router.post("/", function(req, res, next) {
        });
```

1. Move the save subscription saving logic from the GET action to POST and replace it by returning the view with the Subscription object, the state, and the categories from the items.js controller.

 ```javascript
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

                res.render("callback/index", {  
                    title: "Complete Office 365 Connection",
                    subscription: subscription,  
                    categories: items.categories,
                    state: state }); 
            }
        });
        
        /* POST /callback */
        router.post("/", function(req, res, next) {
            //repopulate the subscription for saving
            var subscription = {
                GroupName: req.body.GroupName,
                WebHookUri: req.body.hdnWebhook,
                Category: req.body.cboCategory
            };
    
            //save the subscription and the redirect to state
            db.subscriptions.save(subscription, function(err, sub) {
                if( err || !sub ) {
                    //something went wrong...redirect to error
                    res.redirect("/error?error=subscription not saved");
                }
                else {
                    //redirect to original location (state)
                    res.redirect(req.body.hdnState);
                }
            });
        });
```

1. Next, create a new folder in the **views** folder named **callback** and add an **index.hbs** file in it.

1. Populate index.hbs (in the callback folder) with the following markup.

 ```html
        <h2>Complete Office 365 Connection</h2>
        <form method="POST" action="/callback">
            <div class="row">
                <div class="form-group">
                    <label class="control-label">Office 365 Group</label>
                    <input type="text" name="txtGroup" id="txtGroup" class="form-control" disabled="disabled" value="{{subscription.GroupName}}">
                </div>
            </div>
            <div class="row">
                <div class="form-group">
                    <label class="control-label">Category</label>
                    <select name="cboCategory" id="cboCategory" class="form-control">
                        <option></option>
                        {{#each categories}}
                        <option id="{{.}}">{{.}}</option>
                        {{/each}}
                    </select>
                    <input type="hidden" name="hdnWebhook" id="hdnWebhook" value="{{subscription.WebHookUri}}">
                    <input type="hidden" name="hdnGroupName" id="hdnGroupName" value="{{subscription.GroupName}}">
                    <input type="hidden" name="hdnState" id="hdnState" value="{{state}}">
                </div>
            </div>

            <div class="row">
                <input type="submit" value="Save" class="btn btn-default" />
            </div>
        </form>
 ```
 
1. Next, you need to modify the items.js controller so it only sends Connector messages on subscriptions that meet the category criteria (right now it send messages for all subscriptions on every create). Locate the POST create action and modify the subscription query as follows.
 
```javascript
        db.subscriptions.find({Category: newItem.Category}, function(e, subs) {
```

1. Start debugging the Node.js application using the debug tab in Visual Studio Code or type npm start from a command/terminal prompt. The site should be running at [http://localhost:3000](http://localhost:3000). When you click on **Listings** (and sign-in) the "**Connect to Office 365**" button should display in the header.
 
<a name="Summary" />
## Summary ##

By completing this module, you should have:

- Manually registered an Office 365 Connector and used a webhook to send data into Office 365.
- Connected an existing web application to Office 365 via Office 365 Connectors, including the addition of "Connect to Office 365" buttons, handling callbacks from Office 365, and sending data into Office 365 via webhooks.
- Added additional configuration to the Connector to allow filtering by category.