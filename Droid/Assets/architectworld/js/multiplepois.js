/* Implementation of AR-Experience (aka "World"). */
var World = {
    /* True once data was fetched. */
    initiallyLoadedData: false,

    /* Different POI-Marker assets. */
    markerDrawableIdle: null,
    markerDrawableSelected: null,

    /* List of AR.GeoObjects that are currently shown in the scene / World. */
    markerList: [],

    /* the last selected marker. */
    currentMarker: null,

    /* Called to inject new POI data. */
    loadPoisFromJsonData: function loadPoisFromJsonDataFn(poiData) {
        /* Empty list of visible markers. */
        World.markerList = [];

        /* Start loading marker assets. */
        World.markerDrawableIdle = new AR.ImageResource("assets/marker_idle.png", {
            onError: World.onError
        });
        World.markerDrawableSelected = new AR.ImageResource("assets/marker_selected.png", {
            onError: World.onError
        });

        /* Loop through POI-information and create an AR.GeoObject (=Marker) per POI. */
        for (var currentPlaceNr = 0; currentPlaceNr < poiData.length; currentPlaceNr++) {
            var singlePoi = {
                "id": poiData[currentPlaceNr].id,
                "latitude": parseFloat(poiData[currentPlaceNr].latitude),
                "longitude": parseFloat(poiData[currentPlaceNr].longitude),
                "altitude": parseFloat(poiData[currentPlaceNr].altitude),
                "title": poiData[currentPlaceNr].name,
                "description": poiData[currentPlaceNr].description
            };

            /*alert(singlePoi.id);*/
            /*
                To be able to deselect a marker while the user taps on the empty screen, the World object holds an
                 array that contains each marker.
            */
            World.markerList.push(new Marker(singlePoi));
        }

        World.updateStatusMessage(currentPlaceNr + ' places loaded');
    },

    /* Updates status message shown in small "i"-button aligned bottom center. */
    updateStatusMessage: function updateStatusMessageFn(message, isWarning) {

        var themeToUse = isWarning ? "e" : "c";
        var iconToUse = isWarning ? "alert" : "info";

        $("#status-message").html(message);
        $("#popupInfoButton").buttonMarkup({
            theme: themeToUse,
            icon: iconToUse
        });
    },

    /* Location updates, fired every time you call architectView.setLocation() in native environment. */
    locationChanged: function locationChangedFn(lat, lon, alt, acc) {

        /*if (!World.initiallyLoadedData) {*/
            /*World.requestDataFromLocal(lat, lon);*/
            /*World.requestPersonalInformation('aaaaa', lat, lon);*/
            World.initiallyLoadedData = true;
        /*}*/
    },

    /* Fired when user pressed maker in cam. */
    onMarkerSelected: function onMarkerSelectedFn(marker) {

        /* Deselect previous marker. */
        if (World.currentMarker) {
            if (World.currentMarker.poiData.id === marker.poiData.id) {
                return;
            }
            World.currentMarker.setDeselected(World.currentMarker);
        }

        /* Highlight current one. */
        marker.setSelected(marker);
        World.currentMarker = marker;
    },

    /* Screen was clicked but no geo-object was hit. */
    onScreenClick: function onScreenClickFn() {
        if (World.currentMarker) {
            World.currentMarker.setDeselected(World.currentMarker);
        }
    },

    /* Request POI data. */
    requestDataFromLocal: function requestDataFromLocalFn(centerPointLatitude, centerPointLongitude) {
        var poisToCreate = 20;
        var poiData = [];

        /*poiData.push({
                "id": 1,
                "longitude": (centerPointLongitude + (Math.random() / 5 - 0.1)),
                "latitude": (centerPointLatitude + (Math.random() / 5 - 0.1)),
                "description": ("This is the description of POI#" + 1),
                "altitude": "100.0",
                "name": ("POI#" + 1)
            }); */

            poiData.push({
                "id": 2,
                "longitude": (centerPointLongitude + (Math.random() / 5 - 0.1)),
                "latitude": (centerPointLatitude + (Math.random() / 5 - 0.1)),
                "description": ("This is the description of POI#" + 2),
                "altitude": "100.0",
                "name": ("POI#" + 2)
            });

        World.loadPoisFromJsonData(poiData);
    },
    
    requestPersonalInformation: function requestPersonalInformationFn(name, latitude, longtitude){
        var len = World.markerList.length;
        for(var i = 0;i < length;i++){
            World.markerList[i].remove();
        }

        var url = 'http://koron0902.ddns.net:23456/get_around';

        var json = {
            name : name,
            since : '2018-01-01 00:00:00',
            to : '2018-12-31 23:59:59',
            latitude : latitude,
            longtitude : longtitude,
            distance : 10000000,
            count : 200
        };

        $.ajax({
            type : 'post',
            url : url,
            data : JSON.stringify(json),
            contentType : 'application/json',
            dataType : 'json',
            sctiptCharset : 'utf-8',
            success : function(data) {
                // Success
                World.loadPoisFromJsonData(data);
            },
            error : function(data) {
            }
        });
    },

    onError: function onErrorFn(error) {
        alert(error)
    }
};



/*
    Set a custom function where location changes are forwarded to. There is also a possibility to set
    AR.context.onLocationChanged to null. In this case the function will not be called anymore and no further
    location updates will be received.
*/
AR.context.onLocationChanged = World.locationChanged;

/*
    To detect clicks where no drawable was hit set a custom function on AR.context.onScreenClick where the
    currently selected marker is deselected.
*/
AR.context.onScreenClick = World.onScreenClick;