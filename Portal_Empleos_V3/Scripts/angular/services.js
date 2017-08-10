app.service('mantenedor_total', ['$http', '$mdDialog', function ($http, $mdDialog) {

    this.obtener_datos = function (url) {
        var k = $http.get(url);
        return k;
    };
    this.actualizar_datos = function (url, id, datos) {
        var c = $http.post(url + id, datos);
        return c;
    };
    this.borrar_datos = function (url, id) {
        var b = $http.post(url + id);
        return b;
    };
    this.agregar_datos = function (url, datos) {
        var a = $http.post(url, datos);
        return a;
    };
    this.modal = function (url, id, $scope) {
        $mdDialog.show({
            clickOutsideToClose: true,
            scope: $scope,
            preserveScope: true,
            fullscreen: true,
            templateUrl: url + id,
            controller: function DialogController($scope, $mdDialog) {
                $scope.closeDialog = function () {
                    $mdDialog.hide();
                }
            }
        });
    };
    this.envia_correo = function (id, rut) {
        var ka = $http.post('/soporte/gestor_correos/' + id, rut);
        debugger;
        return ka;
    };


}]);