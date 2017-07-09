Keywords: 2.5d buildings, buildings on map

# Create 2.5D buildings on map

<desc>Two and a half dimensional (shortened to 2.5D, known alternatively as three-quarter perspective and pseudo-3D) is a term used to describe either 2D graphical projections and similar techniques used to cause images or scenes to simulate the appearance of being three-dimensional (3D) when in fact they are not true 3D.</desc> 

This guide represents how to render 2.5D buildings with feature layer on SlimGIS WPF map control. 

In some GIS apps, we want to embed a building block in it which will looks different from the flat map and feel fansion. See the following screenshot.

![2.5D building map](https://github.com/SlimGIS/RenderBuildingDemo-Wpf/raw/master/Previews/building-map-wpf.jpg?raw=true)

Now let's see how we implement it.

## Step 1: Find a building data
We already include a building shapefile in the [project](https://github.com/SlimGIS/RenderBuildingDemo-Wpf). You could also download from [OpenStreetMap Data Extracts](http://download.geofabrik.de/) for free.

## Step 2: Create a feature layer with this building data
This layer will be the provider for fetching building data in the viewport.
```csharp
ShapefileLayer buildingsLayer = new ShapefileLayer(@"SampleData/Buildings.shp");
```

## Step 3: Create an AltitudeFillStyle to apply to the layer
`AltitudeFillStyle` is a FillStyle that could simulate the classic 2.5D rendering for blocks.
```csharp
var altitudeFillStyle = new AltitudeFillStyle(GeoColor.FromHtml("#E6E1DF"), GeoColor.FromHtml("#80D3CDCA"), 1);
altitudeFillStyle.AltitudeUnit = LengthUnit.Meter;
altitudeFillStyle.AltitudeDirection = 90;
altitudeFillStyle.Altitude = 20;
buildingsLayer.Styles.Add(altitudeFillStyle);
```
With this code above, we create an AltitudeFillStyle instance with the fill color and strock color. Then we indicate the altitude direction to the north and the altitude value is 20 meters height.

> Note, the code for step 2- step 3 are all compatible with WinForms and WebAPI.

The rest thing is adding the building to your map. Please check the source code for detail.

## Related Resource
- [Source code](https://github.com/SlimGIS/RenderBuildingDemo-Wpf)
- [Get executable](https://github.com/SlimGIS/RenderBuildingDemo-Wpf/releases)
- [Map Kit WPF](https://slimgis.com/products/wpf)
- [Map Kit Styles](https://slimgis.com/documents/styles)




