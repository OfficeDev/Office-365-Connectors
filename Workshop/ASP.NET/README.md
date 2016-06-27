<a name="HOLTop" />
# Building Office 365 Connectors - ASP.NET #

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

- [Visual Studio Community 2015][1] or greater
- [Office 365 Tenant][2]
- [Any Web Request Composer][2]

[1]: https://www.visualstudio.com/products/visual-studio-community-vs
[2]: https://portal.office.com/Signup?OfferId=B07A1127-DE83-4a6d-9F85-2C104BDAE8B4
[3]: https://www.hurl.it

> **Note:** You can take advantage of the [Visual Studio Dev Essentials]( https://www.visualstudio.com/en-us/products/visual-studio-dev-essentials-vs.aspx) subscription in order to get everything you need to build and deploy your app on any platform.

<a name="Exercises" />
## Exercises ##
This module includes the following exercises:

1. [Getting Started with Office 365 Connectors](#Exercise1)
2. [Leveraging Webhooks with Office 365 Connectors](#Exercise2)
3. [Integrating "Connect to Office 365" into Existing Applications](#Exercise3)
4. [Developing Filtered Connector Subscriptions](#Exercise4)

Estimated time to complete this module: **60 minutes**

>**Note:** When you first start Visual Studio, you must select one of the predefined settings collections. Each predefined collection is designed to match a particular development style and determines window layouts, editor behavior, IntelliSense code snippets, and dialog box options. The procedures in this module describe the actions necessary to accomplish a given task in Visual Studio when using the **General Development Settings** collection. If you choose a different settings collection for your development environment, there may be differences in the steps that you should take into account.

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

1. Open Windows Explorer and browse to the module's **Begin** folder.

1. Double-click the solution file (**BillsListASPNET.sln**) to open the solution in **Visual Studio Community 2015**.

1. The starter solution actually has two project...**BillsListASPNET** (the web application) and **BillsListASPNET.Data** (database project). Right-click the **BillsListASPNET.Data** project and select **Publish**.

1. On the **Publish Database** dialog, click **Edit** to configure the connection information.

	![Publish DB](Images/Mod4_PubDB1.png?raw=true "Publish DB")

	_Publish DB_

1. On the **Connection Properties** dialog enter **(localdb)\MSSQLLocalDB** for the **Server name** and click **OK**.

	![Connection Properties](Images/Mod4_DbCon2.png?raw=true "Connection Properties")

	_Connection Properties_

1. When you return to the **Publish Database** dialog, click the **Publish** button to publish the database to **LocalDb**.

1. When the database has finished publishing, press **F5** or start the debugger to test the starter project.

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
	- **Landing page for your users**: https://localhost:44300
	- **Redirect URL**: https://localhost:44300/callback

1. Save the Form to generate "Connect to Office 365" button markup in step 2 of the form. Copy this markup for use in subsequent steps.

	![](http://i.imgur.com/1InrXFG.png)

	_New Connector Form_

1. Close the browser to stop debugging and open the **_Layout.cshtml** file located in the web project at **Views > Shared**.

1. After the second **navbar-collapse** element (**between lines 27-28**), add following markup as a container for the "Connect to Office 365" button.

```html
        <div class="navbar-collapse collapse" style="padding-top: 5px;">
            <div class="nav navbar-nav navbar-right">
  
            </div>
        </div>	
```

1. Next, paste the "**Connect to Office 365**" button markup generated above into the new div and wrap the entire thing in a authentication check (**Request.IsAuthenticated**). This will only show the button when the user is signed in.

```html
        @if (Request.IsAuthenticated)
        {
        <div class="navbar-collapse collapse" style="padding-top: 5px;">
            <div class="nav navbar-nav navbar-right">
                <a href="https://outlook.office.com/connectors/Connect?state=myAppsState&app_id=a786cbb7-f80d-4968-91c0-9df7a96d75f0&callback_url=https://localhost:44300/callback"><img src="https://o365connectors.blob.core.windows.net/images/ConnectToO365Button.png" alt="Connect to Office 365"></img></a>  
            </div>
        </div>
        }
```

1. Update the **state** URL parameter from **myAppsState** to **@Request.Url.AbsolutePath**

```html
        <a href="https://outlook.office.com/connectors/Connect?state=@Request.Url.AbsolutePath&app_id=a786cbb7-f80d-4968-91c0-9df7a96d75f0&callback_url=https://localhost:44300/callback"><img src="https://o365connectors.blob.core.windows.net/images/ConnectToO365Button.png" alt="Connect to Office 365"></img></a> 
```

1. You might recall we are passing in a callback location of **https://localhost:44300/callback** to Office 365. However, the **Callback** controller does not yet exist...let's create it. Right click the **Controllers** folder in the web project and select **Add > Controller**.

1. Select **MVC Controller - Empty** for the controller type and name it **CallbackController**.

	![New Controller](Images/Mod4_CallbackCtrl.png?raw=true "New Controller")

    _New Controller_

1. Inside the **CallbackController** class, add the following code:

```C#
        // GET: Callback
        public ActionResult Index()
        {
            var error = Request["error"];
            var state = Request["state"];
            if (!String.IsNullOrEmpty(error))
            {
                return RedirectToAction("Error", "Home", null);
            }
            else
            {
                var group = Request["group_name"];
                var webhook = Request["webhook_url"];
                Subscription sub = new Subscription();
                sub.GroupName = group;
                sub.WebHookUri = webhook;

                //save the subscription
                using (BillsListEntities entities = new BillsListEntities())
                {
                    entities.Subscriptions.Add(sub);
                    entities.SaveChanges();
                    return Redirect(state);
                }
            }
        }
```

1. You may need to resolve the following reference after adding the above snippet.

		using BillsListASPNET.Models;

1. The controller looks for information returned from Office 365 and saves it as a subscription. The specific information passed from Office 365 as parameters include:

	- **error**: error details if the connection with Office 365 failed (ex: user rejected the connection)
	- **state**: the state value that was passed in via the "Connect to Office 365" button. In our case, it could include the category the user subscribed to
	- **group_name**: the name of the group the user selected to connect to
	- **webhook_url**: the webhook end-point our application will use to send messages into Office 365

1. Almost done, just need to update the **Create** activity to send messages to the appropriate webhooks. Open the **ItemsController.cs** file located in the **Controllers** folder of the web project.

1. Towards the bottom of the class, add the following code, which resizes the picture, prepares the Connector message, and sends it on the webhook.

```C#
        private async Task callWebhook(string webhook, Item item)
        {
            var imgString = "https://billslist.azurewebsites.net/images/logo_40.png";
            if (Request.Files.Count > 0)
            {
                //resize the image
                Request.Files[0].InputStream.Position = 0;
                Image img = Image.FromStream(Request.Files[0].InputStream);
                var newImg = (Image)(new Bitmap(img, new Size(40, 40)));

                //convert the stream
                using (var stream = new System.IO.MemoryStream())
                {
                    newImg.Save(stream, ImageFormat.Jpeg);
                    stream.Position = 0;
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    imgString = "data:image/jpg;base64, " + Convert.ToBase64String(bytes);
                }
            }

            //prepare the json payload
            var json = @"
                {
                    'summary': 'A new listing was posted to BillsList',
                    'sections': [
                        {
                            'activityTitle': 'New BillsList listing',
                            'activitySubtitle': '" + item.Title + @"',
                            'activityImage': '" + imgString + @"',
                            'facts': [
                                {
                                    'name': 'Category',
                                    'value': '" + item.Category + @"'
                                },
                                {
                                    'name': 'Price',
                                    'value': '$" + item.Price + @"'
                                },
                                {
                                    'name': 'Listed by',
                                    'value': '" + item.Owner + @"'
                                }
                            ]
                        }
                    ],
                    'potentialAction': [
                        {
                            '@context': 'http://schema.org',
                            '@type': 'ViewAction',
                            'name': 'View in BillsList',
                            'target': [
                                'https://localhost:44300/items/detail/" + item.Id + @"'
                            ]
                        }
                    ]}";

            //prepare the http POST
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync(webhook, content))
            {
                //TODO: check response.IsSuccessStatusCode
            }
        }
```

1. You will likely need to resolve a number of references after adding the above snippet.

```C#
		using System.Net.Http;
		using System.Net.Http.Headers;
		using System.Threading.Tasks;
		using System.Drawing;
		using System.Drawing.Imaging;
```
 
1. The snippet takes the new listing details and sends it to Office 365 via **POST** to the webhook end-point.

1. Finally, locate the **Create** activity within the class. **Create** is overloaded, so select the one that is marked with **HttpPost** and has the **Item** parameter. Inside the using statement add the code below between **SaveChanges()** of the new listing and the **RedirectToAction()** statement. This identifies matching subscriptions and calls the appropriate webhooks.

```C#
        //save the item to the database
        using (BillsListEntities entities = new BillsListEntities())
        {
            entities.Items.Add(item);
            var id = entities.SaveChanges();

            //loop through subscriptions and call webhooks for each
            foreach (var sub in entities.Subscriptions)
            {
                await callWebhook(sub.WebHookUri, item);
            }

            return RedirectToAction("Detail", new { id = item.Id });
        }
```

1. It's time to test your work. Press **F5** or start the debugger to launch the application. When you click on **Listings** (and sign-in) the "**Connect to Office 365**" button should display in the header.

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

1. Open the CallbackController.cs file in the Controllers folder and add an additional Index controller action that accepts a Subscription object. Mark this new Index action with the HttpPost directive and the original Index action with HttpGet.

 ```C#
        // GET: Callback
        [Route("callback")]
        [HttpGet]
        public ActionResult Index()
        {
            var error = Request["error"];
            var state = Request["state"];
            if (!String.IsNullOrEmpty(error))
            {
                return RedirectToAction("Error", "Home", null);
            }
            else
            {
                var group = Request["group_name"];
                var webhook = Request["webhook_url"];
                Subscription sub = new Subscription();
                sub.GroupName = group;
                sub.WebHookUri = webhook;

                //save the subscription
                using (BillsListEntities entities = new BillsListEntities())
                {
                    entities.Subscriptions.Add(sub);
                    entities.SaveChanges();
                    return Redirect(state);
                }
            }
        }
        
        // POST: Callback
        [Route("callback")]
        [HttpPost]
        public ActionResult Index(Subscription sub)
        {
        }
```

1. Move the save subscription logic from the HttpGet action to HttpPost and replace it by returning the view with the Subscription object.

 ```C#
        // GET: Callback
        [Route("callback")]
        [HttpGet]
        public ActionResult Index()
        {
            var error = Request["error"];
            var state = Request["state"];
            if (!String.IsNullOrEmpty(error))
            {
                return RedirectToAction("Error", "Home", null);
            }
            else
            {
                var group = Request["group_name"];
                var webhook = Request["webhook_url"];
                Subscription sub = new Subscription();
                sub.GroupName = group;
                sub.WebHookUri = webhook;

                return View(sub);
            }
        }
        
        // POST: Callback
        [Route("callback")]
        [HttpPost]
        public ActionResult Index(Subscription sub)
        {
            //save the subscription
            using (BillsListEntities entities = new BillsListEntities())
            {
                entities.Subscriptions.Add(sub);
                entities.SaveChanges();
                return Redirect(state);
            }
        }
```

1. Next, return the state and list of categories as ViewData before returning the view in the HttpGet Index action.

 ```C#
        //return the partial subscription and add state and categories to ViewData
        ViewData.Add("state", state);
        ViewData.Add("categories", ItemsController.categories);
        return View(sub);
 ```
 
 1. The HttpPost action returns Redirect(state), but the state variable does not exist. Modify the code to read state from the Request object.
 
 ```C#
        [Route("callback")]
        [HttpPost]
        public ActionResult Index(Subscription sub)
        {
            //save the subscription
            using (BillsListEntities entities = new BillsListEntities())
            {
                entities.Subscriptions.Add(sub);
                entities.SaveChanges();

                //redirect back to the original location the user was at
                return Redirect(Request["state"]);
            }
        }
```
 
1. Next, you need to create a View for the Callback Controller that will allow a user to select a category. Right-click the Views/Callback folder and select Add > View and call the view Index.
 
1. Populate the Index view with the following markup.
 
```HTML
@{
    ViewBag.Title = "Complete Office 365 Connection";
}
@model BillsListASPNET.Models.Subscription

<h2>Complete Office 365 Connection</h2>
@using (Html.BeginForm("index", "callback", FormMethod.Post, new { enctype = "multipart/form-data" }))
{
    <div class="row">
        <div class="form-group">
            <label class="control-label">Office 365 Group</label>
            @Html.EditorFor(model => model.GroupName, new { htmlAttributes = new { @class = "form-control", disabled = "disabled" } })
        </div>
    </div>
    <div class="row">
        <div class="form-group">
            <label class="control-label">Category</label>
            @Html.DropDownListFor(model => model.Category, new SelectList((List<string>)ViewData["categories"]), new { @class = "form-control" })
            @Html.HiddenFor(model => model.WebHookUri)
            @Html.HiddenFor(model => model.GroupName)
            @Html.Hidden("state", ViewData["state"].ToString())
        </div>
    </div>

    <div class="row">
        <input type="submit" value="Save" class="btn btn-default" />
    </div>
}
```

1. Next, you need to modify the ItemsController so it only sends Connector messages on subscriptions that meet the category criteria (right now it send messages for all subscriptions on every create). Locate the HttpPost Create action and modify the subscription sending section as follows.
 
```C#
        //loop through subscriptions and call webhooks for each where category matches
        var subs = entities.Subscriptions.Where(i => i.Category == item.Category);
        foreach (var sub in subs)
        {
            await callWebhook(sub.WebHookUri, item);
        }
```

1. Start debugging the solution by clicking the start button or F5. After signing into the application, click on the Connect to Office 365 button. This time the Connector consent flow should allow you to select a category for the subscription.
 
<a name="Summary" />
## Summary ##

By completing this module, you should have:

- Manually registered an Office 365 Connector and used a webhook to send data into Office 365.
- Connected an existing web application to Office 365 via Office 365 Connectors, including the addition of "Connect to Office 365" buttons, handling callbacks from Office 365, and sending data into Office 365 via webhooks.
- Added additional configuration to the Connector to allow filtering by category.

> **Note:** You can take advantage of the [Visual Studio Dev Essentials]( https://www.visualstudio.com/en-us/products/visual-studio-dev-essentials-vs.aspx) subscription in order to get everything you need to build and deploy your app on any platform.
