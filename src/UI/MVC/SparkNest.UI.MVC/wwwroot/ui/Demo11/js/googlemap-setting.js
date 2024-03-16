let map;

async function initMap() {
  // The location of London
  const position_1 = { lat: 51.498265, lng: -0.131050 };
  // The location of Istanbul
  const position_2 = { lat: 41.045762, lng: 28.986908 };
  // The location of New York
  const position_3 = { lat: 40.696274, lng: -73.815717 };
  
  // Request needed libraries.
  //@ts-ignore
  const { Map } = await google.maps.importLibrary("maps");
  const { Marker } = await google.maps.importLibrary("marker");
  
  const styledMapType = new google.maps.StyledMapType(
    [
      {
        "featureType": "water",
        "elementType": "geometry.fill",
        "stylers": [
          {
              "color": "#d3d3d3"
          }
        ]
      },
      {
        "featureType": "transit",
        "stylers": [
          {
              "color": "#808080"
          },
          {
              "visibility": "off"
          }
        ]
      },
      {
        "featureType": "road.highway",
        "elementType": "geometry.stroke",
        "stylers": [
          {
              "visibility": "on"
          },
          {
              "color": "#b3b3b3"
          }
        ]
      },
      {
        "featureType": "road.highway",
        "elementType": "geometry.fill",
        "stylers": [
          {
              "color": "#ffffff"
          }
        ]
      },
      {
        "featureType": "road.local",
        "elementType": "geometry.fill",
        "stylers": [
          {
              "visibility": "on"
          },
          {
              "color": "#ffffff"
          },
          {
              "weight": 1.8
          }
        ]
      },
      {
        "featureType": "road.local",
        "elementType": "geometry.stroke",
        "stylers": [
          {
              "color": "#d7d7d7"
          }
        ]
      },
      {
        "featureType": "poi",
        "elementType": "geometry.fill",
        "stylers": [
          {
              "visibility": "on"
          },
          {
              "color": "#ebebeb"
          }
        ]
      },
      {
        "featureType": "administrative",
        "elementType": "geometry",
        "stylers": [
          {
              "color": "#a7a7a7"
          }
        ]
      },
      {
        "featureType": "road.arterial",
        "elementType": "geometry.fill",
        "stylers": [
          {
              "color": "#ffffff"
          }
        ]
      },
      {
        "featureType": "road.arterial",
        "elementType": "geometry.fill",
        "stylers": [
          {
              "color": "#ffffff"
          }
        ]
      },
      {
        "featureType": "landscape",
        "elementType": "geometry.fill",
        "stylers": [
          {
              "visibility": "on"
          },
          {
              "color": "#efefef"
          }
        ]
      },
      {
        "featureType": "road",
        "elementType": "labels.text.fill",
        "stylers": [
          {
              "color": "#696969"
          }
        ]
      },
      {
        "featureType": "administrative",
        "elementType": "labels.text.fill",
        "stylers": [
          {
              "visibility": "on"
          },
          {
              "color": "#737373"
          }
        ]
      },
      {
        "featureType": "poi",
        "elementType": "labels.icon",
        "stylers": [
          {
              "visibility": "off"
          }
        ]
      },
      {
        "featureType": "poi",
        "elementType": "labels",
        "stylers": [
          {
              "visibility": "off"
          }
        ]
      },
      {
        "featureType": "road.arterial",
        "elementType": "geometry.stroke",
        "stylers": [
          {
              "color": "#d6d6d6"
          }
        ]
      },
      {
        "featureType": "road",
        "elementType": "labels.icon",
        "stylers": [
          {
              "visibility": "off"
          }
        ]
      },
      {},
      {
        "featureType": "poi",
        "elementType": "geometry.fill",
        "stylers": [
          {
              "color": "#dadada"
          }
        ]
      }
    ],
    { name: "Map" },
  );

  // The map, centered at Uluru
  map = new Map(document.getElementById("map"), {
    zoom: 3,
    center: position_1,
    mapId: "UomoMap",
    mapTypeControlOptions: {
      mapTypeIds: ["styled_map"],
    },
  });

  map.mapTypes.set("styled_map", styledMapType);
  map.setMapTypeId("styled_map");

  // The marker, positioned at London
  const marker_1 = new Marker({
    map: map,
    position: position_1,
    icon: "../images/map-marker.png",
    title: "London",
  });
  // The marker, positioned at Istanbul
  const marker_2 = new Marker({
    map: map,
    position: position_2,
    icon: "../images/map-marker.png",
    title: "Istanbul",
  });
  // The marker, positioned at New York
  const marker_3 = new Marker({
    map: map,
    position: position_3,
    icon: "../images/map-marker.png",
    title: "New York",
  });

  let store_info = document.querySelector(".google-map__marker-detail");

  let store_selector_1 = document.querySelector("#store_selector_1");
  if(store_selector_1) {
    store_selector_1.addEventListener("click", function(e) {
      store_info.classList.remove("hide");
      map.setCenter(marker_1.getPosition());
      map.setZoom(12);
      document.querySelector(".google-map__marker-detail__content").innerHTML = store_selector_1.closest('.store-location__search-result__item').innerHTML;
    }, false);
  }

  let store_selector_2 = document.querySelector("#store_selector_2");
  if(store_selector_2) {
    store_selector_2.addEventListener("click", function(e) {
      store_info.classList.remove("hide");
      map.setCenter(marker_2.getPosition());
      map.setZoom(12);
      document.querySelector(".google-map__marker-detail__content").innerHTML = store_selector_2.closest('.store-location__search-result__item').innerHTML;
    }, false);
  }

  let store_selector_3 = document.querySelector("#store_selector_3");
  if(store_selector_3) {
    store_selector_3.addEventListener("click", function(e) {
      store_info.classList.remove("hide");
      map.setCenter(marker_3.getPosition());
      map.setZoom(12);
      document.querySelector(".google-map__marker-detail__content").innerHTML = store_selector_3.closest('.store-location__search-result__item').innerHTML;
    }, false);
  }

  let store_info_close_btn = document.querySelector(".google-map__marker-detail .btn-close");
  if(store_info_close_btn && store_info) {
    store_info_close_btn.addEventListener("click", function(e) {
      store_info.classList.add("hide");
    }, false);
  }
}

initMap();