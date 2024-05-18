var bweInterop = {};
// ReSharper disable once ExperimentalFeature
bweInterop.getPosition = async function () {
   function getPositionAsync() {
      return new Promise((success, error) => {
         navigator.geolocation.getCurrentPosition
            (success, error);
      });
   }
   if (navigator.geolocation) {
// ReSharper disable once ExperimentalFeature
      var position = await getPositionAsync();
      var coords = {
         latitude: position.coords.latitude,
         longitude: position.coords.longitude
      };
      return coords;
   } else {
      throw Error("Geolocation is not supported.");
   };
}