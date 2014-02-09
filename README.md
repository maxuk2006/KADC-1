KADC
====

A Knights &amp; Dragons (Android and iOS game published by Gree) Calculator web application.

====

Currently this site is available at http://kadc.azurewebsites.net/ .   I no longer intend to update or support it so I am making the code availabe here.  Feel free to use and host the code as you wish and sorry for the state of the code and some of the dubious design decisions I made.

====

Some pointers for updating data used by this application:

* In the App_Data folder there are 3 xml files called armors.xml, levels.xml and logs.xml.  Add to or edit these files and the site will reflect those changes. 
* If you add a new armor to the armors.xml file you will also need to add an icon to the Images\Icon folder.  The image should be 24x24 pixels, in PNG format, and the file name should be the same as armor name with spaces or special symbols.  For example an armor called Wanderer's Shroud would have an icon called WanderersShroud.png .
* There is some vestigial C# code related to initializing armor and level data.  There is no point updating this as it is no longer used.
