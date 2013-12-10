tbldefexport
============

This code was crafted by jbennie.me (est 2004) to extract data structures from access in a simple semi-scripted way. 
The code is provided as is and works in access versons 2000 - 2012, it may also work on earlier versions. any type erros/warrnings  
will be resolved by including Access / data access SDKs via the vba tools menu  
The code is not under licence and can be used freely to create any derivative works.


To use: 

Import the script to access. modife the file to export the tables required by name, place cursor at the brginning of the file and play or create a macro to run. 
The output format can be any text format, currently the format extracted is a personal DDL format for the c# class libraries i will share here. These extend the simple operations of the system.Data containers. (Functionaly now surpassed by LINQ techniques ... but this is better for volumn data loading and migration/prototyping work)



