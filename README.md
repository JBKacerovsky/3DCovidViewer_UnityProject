# 3DCovidViewer_UnityProject
Building a Unity app that downloads and displays the covid19 database from http://ourworldindata.org

The goal of this "game"/app is to access the current Our World in Data COVID-19 database and to allow the user to select multiple country specific datasets, to display the data in 3D. 

A working version of the Viewer can be accessed here: [3DCovidViewer](https://jbkacerovsky.github.io/3DCovidViewer/)
the most current WebGL build here: [current build](https://github.com/JBKacerovsky/3DCovidViewer)

[ourworldindata.org](https://ourworldindata.org/coronavirus) does amazing work compiling this data daily and providing it [free](https://github.com/owid/covid-19-data/tree/master/public/data) to use. They offer a number of fantastic 2D interactive charts on their website. 

With this app I wanted to explore using Unity3D to view and navigate these datasets in 3D. This is still a very unfinished and experimental project. Mostly this is a toy project where I wanted to see whether I could use Unity3D in this way, to load publicly available datasets from an online source, parse the data in c#, and build an interface to display the data in 3D

The data (online .csv text file) is loaded durign startup and parsed into a list based datastructure. Through a UI the user can select a country and 2 data dimensions and add the data to the current view. The data will be displayed as a sreies of spheres in the 3D environment. Each sphere (along the x axis) represents one day. The first selected data dimension will be displayed on the y-axis, as height of the spheres, while the second dimension defines the size of the spheres. Data-dimensions are displayed as realative height/size not as absolute values (to deal with the extremely large range of values). The scale is defined by the first dataset added to thee current view. Two text boxes allow the user to adjust the scale for the next series to be added. 

The WebGL implementation was built using the [better-unity-webgl-template](https://github.com/greggman/better-unity-webgl-template), which worked amazingly well for me. 

## To Do
- add axes, lables, references. 
- ensure data alignement between datasets with different starting points. 
- add lables to datasets
- deal with empty cells (many of the data categories are not populated for all countries. currently this will result in sphere size 0 and invisible data points. 
	- add warning message for empty data points
	- add default value?
- add option to interactively display more information about specific datasets/datapoints.  
- rethink scaling? should the view dynamically or interactively rescale as more data is added? or should the first selected dataset always define the scale. 


# Data Source
> Max Roser, Hannah Ritchie, Esteban Ortiz-Ospina and Joe Hasell (2020) - "Coronavirus Pandemic (COVID-19)". Published online at OurWorldInData.org. Retrieved from: '[https://ourworldindata.org/coronavirus](https://ourworldindata.org/coronavirus)' [Online Resource] 
