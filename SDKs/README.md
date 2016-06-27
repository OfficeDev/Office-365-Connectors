# O365-Connectors-SDK
Office 365 Connectors are a powerful way to send useful 3rd party information and content into Office 365 where users can have conversations, collaborate and take action on the information. JSON data is sent into Connectors using a specific schema outline [HERE](https://dev.outlook.com/Connectors/Reference). However, .NET isn't the best platform for working with JSON, so this SDK was built to easily construct a message and send it into Office 365.

To send a message into Office 365 using a Connector, leverage Office365ConnectorSDK.Message class. Populate it with the desired attributes and then use the await message.Send method (which accepts the Connector webhook URI as a parameter). You can also call message.ToJSON() to get just the JSON payload for the message.

**The following strongly-typed Message class**
<pre>
Message message = new Message()
{
    summary = "Miguel Garcia commented on Trello",
    title = "Project Tango",
    sections = new List&lt;Section&gt;() {
        new Section() {
            activityTitle = "Miguel Garcia commented",
            activitySubtitle = "On Project Tango",
            activityText = "\"Here are the designs \"",
            activityImage = "http://connectorsdemo.azurewebsites.net/images/MSC12_Oscar_002.jpg"
        }, new Section() {
            title = "Details",
            facts = new List&lt;Fact&gt;() {
                new Fact("Labels", "Designs, redlines"),
                new Fact("Due date", "Dec 7, 2016"),
                new Fact("Attachments", "[final.jpg](http://connectorsdemo.azurewebsites.net/images/WIN14_Jan_04.jpg)")
            }
        }, new Section() {
            title = "Images",
            images = new List&lt;Image&gt;() {
                new Image("http://connectorsdemo.azurewebsites.net/images/MicrosoftSurface_024_Cafe_OH-06315_VS_R1c.jpg"),
                new Image("http://connectorsdemo.azurewebsites.net/images/WIN12_Scene_01.jpg"),
                new Image("http://connectorsdemo.azurewebsites.net/images/WIN12_Anthony_02.jpg")
            }
        } }, 
    potentialAction = new List<PotentialAction>() {
        new PotentialAction()
        {
            name = "View in Trello",
            target = new List<string>() { "https://trello.com/c/1101/" }
        }
    }
};
var result = await m.Send("Connector Webhook URI");
</pre>

**Produces the following JSON**
<pre>
{
  "summary": "Miguel Garcia commented on Trello",
  "title": "Project Tango",
  "sections": [
    {
      "activityTitle": "Miguel Garcia commented",
      "activitySubtitle": "On Project Tango",
      "activityText": "\"Here are the designs \"",
      "activityImage": "http://connectorsdemo.azurewebsites.net/images/MSC12_Oscar_002.jpg",
    },
    {
      "title": "Details",
      "facts": [
        {
          "name": "Labels",
          "value": "Designs, redlines"
        },
        {
          "name": "Due date",
          "value": "Dec 7, 2016"
        },
        {
          "name": "Attachments",
          "value": "[final.jpg](http://connectorsdemo.azurewebsites.net/images/WIN14_Jan_04.jpg)"
        }
      ]
    },
      {
        "title": "Images",
        "images": [
          {
          "image":"http://connectorsdemo.azurewebsites.net/images/MicrosoftSurface_024_Cafe_OH-06315_VS_R1c.jpg"
          },
          {
          "image":"http://connectorsdemo.azurewebsites.net/images/WIN12_Scene_01.jpg"
          },
          {
          "image":"http://connectorsdemo.azurewebsites.net/images/WIN12_Anthony_02.jpg"
          }
        ]
      }
  ],
  "potentialAction": [
    {
      "@context": "http://schema.org",
      "@type": "ViewAction",
      "name": "View in Trello",
      "target": [
        "https://trello.com/c/1101/"
      ]
    }
  ]
}
</pre>

**Which results in the following message in Office 365**
![](http://i.imgur.com/eNTAiWM.png)