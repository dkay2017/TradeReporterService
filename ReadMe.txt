ReadMe of the Trade Reporter Service.

Assumptions / Considerations  :
        - The Trade Volume are deliberally rounded.
		- The GetTrade function can return collection of multiple Power periods object.
		- The GetTrade return volume for each 24 period without any missing value. ( Hence no check has been made) 


Description : 
  A) The complete logic is spit into two components
   1) Aggregrator logic 
   2) CSV Report Generator   
   
  B) The Unit Tests are build around the main logic  Aggregator Component .
  C) DI is deliberally not written as it was NOT necessary 
  D) App Setting has two keys 
      1) Setting the interval in sec for generation of Report
      2) Path of the File

  E) There is a Log written in the path/Log folder	 
      

Instructions : 
  A) Installation :
   1)  Do a Release Build  
   1.B) Go to the Release Folder on Command Prompt admin mode.
   2) Register the Service using the following command  ( Admin Mode) 
      installutil.exe traderreporterservice.exe 	 
   3) Start the service from windows service console panel ( msc.service command prompt)
   
 B)  Uninstallation  :
   1) Stop the Service  from Service Console Panel
   1.B) Go to the Release Folder on Command Prompt admin mode.
   2) UnRegister the Service using the following command  ( Admin Mode) 
      installutil.exe -u traderreporterservice.exe  	  
	  
  C) The Trade Volume CSV File is Generated in the Path defined in App.Setting   
  D) The log file is is Generated in the Path/Log
    