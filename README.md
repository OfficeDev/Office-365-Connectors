<a name="Top" />
# Office 365 Connectors #

---

## Overview ##
This repository contains technical documentation, samples, labs, SDKs, and more related to Office 365 Connectors. Below you will find a comprehsive list of assets available in this repo.

- [Technical PowerPoint Presentation](#PPTX)
- [Short 100-Level Overview Video](#Vid1)
- [50min 200-Level Deep-dive Video](#Vid2)
- [ASP.NET Connector Lab](#AspLab)
- [NodeJS Connector Lab](#NodeLab)
- [ASP.NET Connector Sample](#AspSample)
- [NodeJS Connector Sample](#NodeSample)
- [Office 365 Connectors SDK for .NET](#ConnectorsSDK)


<a name="PPTX" />
## Technical PowerPoint Presentation ##
The following PowerPoint Presentation provides a technical deep-dive into building Office 365 Connectors, including an overview of Connectors, development, and adnvanced scenarios.
[![](http://i.imgur.com/ThUL6mZ.png)](https://github.com/OfficeDev/Office-365-Connectors/blob/master/Deep%20Dive%20into%20Office%20365%20Connectors.pptx?raw=true)
[Back to top](#Top)

<a name="Vid1" />
## Short 100-Level Overview Video ##
This episode of the Office Dev Show provides a high-level glimpse into building Office 365 Connectors.
[![](http://i.imgur.com/9egmuJa.png)](https://channel9.msdn.com/Shows/Office-Dev-Show/Office-Dev-Show-Episode-30-Getting-Started-with-Office-365-Connectors)
[Back to top](#Top)

<a name="Vid2" />
## 50min 200-Level Deep-dive Video ##
This deep-dive video provides a comprehensive overview of Office 365 Connectors, what they are, how to build them, and some interesting advanced scenarios.
[![](http://i.imgur.com/d8vRBOx.png)](https://channel9.msdn.com/Series/Office-365-Dev-Series/Deep-dive-into-building-Office-365-Connectors)
[Back to top](#Top)

<a name="AspLab" />
## ASP.NET Connector Lab ##
In this lab you will convert an existing ASP.NET MVC application to integrate with Office 365 Connectors. The application is a ficticious online garage sale site similar to Craigslist (but called Billslist in honor of Bill Gates). You will modify with the "Connect to Office 365" button and handle a Connector flow. You will store the Connector webhooks and call them when new items are created in the system. Finally, you will modify the Connector callback to provide additional subscription filter (ex: filter by category). 

[https://github.com/OfficeDev/Office-365-Connectors/tree/master/Workshop/ASP.NET](https://github.com/OfficeDev/Office-365-Connectors/tree/master/Workshop/ASP.NET)

[Back to top](#Top)

<a name="NodeLab" />
## NodeJS Connector Lab ##
In this lab you will convert an existing Node.js/Express/Handlebars application to integrate with Office 365 Connectors. The application is a ficticious online garage sale site similar to Craigslist (but called Billslist in honor of Bill Gates). You will modify with the "Connect to Office 365" button and handle a Connector flow. You will store the Connector webhooks and call them when new items are created in the system. Finally, you will modify the Connector callback to provide additional subscription filter (ex: filter by category).

[https://github.com/OfficeDev/Office-365-Connectors/tree/master/Workshop/NodeJS](https://github.com/OfficeDev/Office-365-Connectors/tree/master/Workshop/NodeJS)

[Back to top](#Top)

<a name="AspSample" />
## ASP.NET Connector Sample ##
This completed code sample is written in ASP.NET MVC and provides a ficticious online garage sale site similar to Craigslist (but called Billslist in honor of Bill Gates). It allows users to post items for sale in a number of categories. Users can also integrate Office 365 Connectors for specific categories. Have an Office 365 Group/Community of musicians...try subscribing to musical instruments posts.

[https://github.com/OfficeDev/Office-365-Connectors/tree/master/Workshop/ASP.NET/End-EX4](https://github.com/OfficeDev/Office-365-Connectors/tree/master/Workshop/ASP.NET/End-EX4)

[Back to top](#Top)

<a name="NodeSample" />
## NodeJS Connector Sample ##
This completed code sample is written in Node.js/Express/Handlebars and provides a ficticious online garage sale site similar to Craigslist (but called Billslist in honor of Bill Gates). It allows users to post items for sale in a number of categories. Users can also integrate Office 365 Connectors for specific categories. Have an Office 365 Group/Community of musicians...try subscribing to musical instruments posts.

[https://github.com/OfficeDev/Office-365-Connectors/tree/master/Workshop/NodeJS/End-EX4](https://github.com/OfficeDev/Office-365-Connectors/tree/master/Workshop/NodeJS/End-EX4)

[Back to top](#Top)

<a name="ConnectorsSDK" />
## Office 365 Connectors SDK for .NET ##
Office 365 Connectors are a powerful way to send useful 3rd party information and content into Office 365 where users can have conversations, collaborate and take action on the information. JSON data is sent into Connectors using a specific schema outline [HERE](https://dev.outlook.com/Connectors/Reference). However, .NET isn't the best platform for working with JSON, so this SDK was built to easily construct Connector messages and send them into Office 365.

[https://github.com/OfficeDev/Office-365-Connectors/tree/master/SDKs](https://github.com/OfficeDev/Office-365-Connectors/tree/master/SDKs)

[Back to top](#Top)


This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information, see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
