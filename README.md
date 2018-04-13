SIFEI
=====

Spreadsheet Inspection Framework Excel Integration

This project provides a Microsoft Excel AddIn for the Spreadsheet Inspection Framework.
More documentation is available from the Spreadsheet Inspection Framework project:

https://github.com/kuleszdl/Spreadsheet-Inspection-Framework

Licence
-------

Spreadsheet Inspection Framework Excel Integration is avaible under the GNU General Public License Version 3. For details see LICENSE file.

Prerequisites
-------------

Installed Visual Studio 2013 (Update 5 tested) with Office Developer Tools

Download
--------

```Shell
git clone https://github.com/kuleszdl/SIFEI.git
```

Build
-----

* Open SIF.Visualization.Excel.sln in the repository with Visual Studio.
* Run "Build" -> "Build solution".

Run
---

* Start debug session (green arrow "Run"). Excel 2013 will open with SIFEI loaded.
* Open or create a spreadsheet and select the "Inspection"-Ribbon.
* Enter correct SIFCore URL in the "Global Settings" dialog.
* Configure Inspections with the "Policy Configuration" dialog.

Create setup package
--------------------

You have to sign the solution with a newly created test certificate to create a package which can be installed on other machines.
Afterwards just run "Create Setup Project" from the "Build" menu.