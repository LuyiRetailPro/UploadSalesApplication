This is an application developed for V9 in UnderArmour HQ.

It is used to convert sales excel files from franchise partners into INVOICE.XML files.

Before it creates the Inventory.xml file, it will first check all the item_sids and alu against Retail Pro database to see if they are valid. 
If ALU does not match item_sid, plugin will use ALU instead. 
If there is any item that is not valid for both ALU and item_sid, the application will not generate any invoice.xml.
If the date in sales excel is after today, the applciaiton will not process with this entry and write into log. 

Each franchise partner is 1 subsidiary in Retail Pro, and the configuration of file location output, input, archive can be done in the settings form. 
