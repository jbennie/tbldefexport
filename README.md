tbldefexport
============

//the vba script. 
This code was crafted, created and enhanced by jbennie.me from distilled prior art found on the internet (est 2011) to extract data structures from access in a simple semi-scripted way. 
The code is provided as is and works in access versons 2000 - 2012, it may also work on earlier versions. Any type errors or warrningscan usually be resolved by including Access / data access SDKs via the vba tools menu  

//the c sharp code
Is the personal creative work of jbennie.me (Joseph Bennie)
The code is my personal code and by sharing here I am not giving or forfieting my right to use this code for profit.
The code may be used under terms of the Apache Licence. 

To use: 

Import the script into access. Modify the file to export the tables required by name, place cursor at the beginning of the file and play or create a macro to run. 
The output format can be any text format, currently the format extracted is a personal DDL format for the c# class libraries I will also share here. These extend the simple operations of the system.Data containers. (Functionaly now surpassed by LINQ techniques ... but this is better for volumn data loading(1) and migration(2)/prototyping work)

1) not very high transaction daily data loading ... but you can at your risk. 
2) it was built to migrate and dynamically remodel data into new structured via custom c# progs.



