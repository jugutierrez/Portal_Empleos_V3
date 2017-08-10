




app.controller('ExampleController', ['$scope', function ($scope) {
    $scope.loading = false;
    $scope.templates =
      [{ name: 'template1.html', url: '/panel/index' },
       { name: 'template2.html', url: '/panel/mis_bodegas' },
      { name: 'template3.html', url: '/panel/informes' }];

    $scope.template = $scope.templates[0];
    $scope.boton = function (url) {
        $scope.loading = true;
        $scope.template = $scope.templates[url];
        $scope.loading = false;
    };
}]);

app.controller('AppCtrl', function ($scope, $timeout, $mdSidenav, $log) {
    $scope.toggleLeft = buildDelayedToggler('left');
    $scope.toggleRight = buildToggler('right');
    $scope.isOpenRight = function () {
        return $mdSidenav('right').isOpen();
    };

    /**
     * Supplies a function that will continue to operate until the
     * time is up.
     */
    function debounce(func, wait, context) {
        var timer;

        return function debounced() {
            var context = $scope,
                args = Array.prototype.slice.call(arguments);
            $timeout.cancel(timer);
            timer = $timeout(function () {
                timer = undefined;
                func.apply(context, args);
            }, wait || 10);
        };
    }

    /**
     * Build handler to open/close a SideNav; when animation finishes
     * report completion in console
     */
    function buildDelayedToggler(navID) {
        return debounce(function () {
            // Component lookup should always be available since we are not using `ng-if`
            $mdSidenav(navID)
              .toggle()
              .then(function () {
                  $log.debug("toggle " + navID + " is done");
              });
        }, 200);
    }

    function buildToggler(navID) {
        return function () {
            // Component lookup should always be available since we are not using `ng-if`
            $mdSidenav(navID)
              .toggle()
              .then(function () {
                  $log.debug("toggle " + navID + " is done");
              });
        };
    }
})
.controller('LeftCtrl', function ($scope, $timeout, $mdSidenav, $log) {
    $scope.close = function () {
        // Component lookup should always be available since we are not using `ng-if`
        $mdSidenav('left').close()
          .then(function () {
              $log.debug("close LEFT is done");
          });

    };
})
.controller('RightCtrl', function ($scope, $timeout, $mdSidenav, $log) {
    $scope.close = function () {
        // Component lookup should always be available since we are not using `ng-if`
        $mdSidenav('right').close()
          .then(function () {
              $log.debug("close RIGHT is done");
          });
    };
});

app.controller('HomeCtrl', ['$scope', '$http', 'mantenedor_total', function ($scope, $http, mantenedor_total) {


    $scope.prof_curriculum = {};
    $scope.exp_curriculum = {};
    $scope.est_curriculum = {};
    $scope.soft_curriculum = {};
    $scope.hab_curriculum = {};
    $scope.tipo_identificacion = {};
    $scope.doc_curriculum = {};
    $scope.rellenar_h = {};
    $scope.rellenar_id = {};
    $scope.rellenar_n_id = {};
    $scope.rellenar_pro = {};
    $scope.rellenar_sof = {};
    $scope.rellenar_g_sof = {};
    $scope.rellenar_insti = {};
    $scope.rellenar_capa = {};
    $scope.rellenar_e_capa = {};
    $scope.rellenar_cargo_exp = {};
    $scope.rellenar_area_exp = {};
    $scope.rellenar_reg = {};
    $scope.rellenar_ge = {};
    $scope.rellenar_dis = {};
    $scope.rellenar_city = {};
    $scope.respuestas = {};


    $scope.datos_persona = function () {

        $scope.load_datos = true;
        $scope.error_datos = true;
        var d_p = mantenedor_total.obtener_datos('/Curriculum_mant/datos_persona_curriculum');
        d_p.then(function (successResponse) {
            if (successResponse.data.success == true) {
                $scope.datos_per = successResponse.data.datos_p;
                $scope.load_datos = false;
            }
            else {
                $scope.load_datos = false;
            }
        },
        function (errorResponse) {
            $scope.error_datos = false;
            $scope.load_datos = false;
        });
    };

    $scope.foto_persona = function () {
        $scope.ver_foto = true;
        $scope.error_foto = true;
        var f_p = mantenedor_total.obtener_datos('/Curriculum_mant/llamar_foto_curriculum');
        f_p.then(function (successResponse) {
            if (successResponse.data.success == true) {
                $scope.foto_per = successResponse.data.foto_v;
                $scope.ver_foto = false;
            }
            else {
                $scope.ver_foto = false;
            }
        },
        function (errorResponse) {
            $scope.error_foto = false;
            $scope.ver_foto = false;
        });
    };

    $scope.identificacion_persona = function () {
        $scope.carga_i_p = true;
        $scope.error_ip = true;
        $scope.dif = false;
        var i_p = mantenedor_total.obtener_datos('/Curriculum_mant/listar_tipo_identificacion_persona');
        i_p.then(function (successResponse) {
            $scope.tipo_identificacion = successResponse.data.tipo_i;
            $scope.carga_i_p = false;
            $scope.dif = true;
        },
         function (errorResponse) {
             $scope.carga_i_p = false;
             $scope.error_ip = false;
         });
    };

    $scope.rellenar_habilidad = function () {

        $scope.carga_r_h = true;
        $scope.error_r_h = true;
        $scope.dif_r_h = false;
        var r_h = mantenedor_total.obtener_datos('/Curriculum_mant/rellenar_habilidad');
        r_h.then(function (successResponse) {
            $scope.rellenar_h = successResponse.data.rellenar_ha;
            $scope.carga_r_h = false;
            $scope.dif_r_h = true;
        },
        function (errorResponse) {
            $scope.carga_r_h = false;
            $scope.error_r_h = false;
            console.log("error rellena habilidad");
        });
    };

    $scope.rellenar_grado_habilidad = function () {
        $scope.carga_r_gh = true;
        $scope.error_r_gh = true;
        $scope.dif_r_gh = false;
        var r_gh = mantenedor_total.obtener_datos('/Curriculum_mant/rellenar_grado_habilidad');
        r_gh.then(function (successResponse) {
            $scope.rellenar_grado_h = successResponse.data.rellenar_grado_ha;
            $scope.carga_r_gh = false;
            $scope.dif_r_gh = true;
        },
        function (errorResponse) {
            $scope.carga_r_gh = false;
            $scope.error_r_gh = false;
            console.log("error rellena grado habilidad");
        });
    };

    $scope.rellenar_idioma = function () {
        $scope.carga_r_id = true;
        $scope.error_r_id = true;
        $scope.dif_r_id = false;
        var r_i = mantenedor_total.obtener_datos('/Curriculum_mant/rellenar_idiomas');
        r_i.then(function (successResponse) {
            $scope.rellenar_id = successResponse.data.rellenar_idioma;
            $scope.carga_r_id = false;
            $scope.dif_r_id = true;
        },
         function (errorResponse) {
             $scope.carga_r_id = false;
             $scope.error_r_id = false;
             console.log("error rellena idiomas");
         });
    };

    $scope.rellenar_nivel_idiomas = function () {
        $scope.carga_r_ni = true;
        $scope.error_r_ni = true;
        $scope.dif_r_ni = false;
        var r_ni = mantenedor_total.obtener_datos('/Curriculum_mant/rellenar_nivel_idiomas');
        r_ni.then(function (successResponse) {
            $scope.rellenar_n_id = successResponse.data.rellenar_n_idioma;
            $scope.carga_r_ni = false;
            $scope.dif_r_ni = true;
        },
        function (errorResponse) {
            $scope.carga_r_ni = false;
            $scope.error_r_ni = false;
            console.log("error rellena nivel idiomas");
        });
    };

    $scope.rellenar_profesiones = function () {
        $scope.carga_r_p = true;
        $scope.error_r_p = true;
        $scope.dif_r_p = false;
        var r_p = mantenedor_total.obtener_datos('/Curriculum_mant/rellenar_profesiones');
        r_p.then(function (successResponse) {
            $scope.rellenar_pro = successResponse.data.rellenar_p;
            $scope.carga_r_p = false;
            $scope.dif_r_p = true;
        },
         function (errorResponse) {
             $scope.carga_r_p = false;
             $scope.error_r_p = false;
             console.log("error rellena profesiones");
         });
    };

    $scope.rellenar_softwares = function () {
        $scope.carga_r_s = true;
        $scope.error_r_s = true;
        $scope.dif_r_s = false;
        var r_s = mantenedor_total.obtener_datos('/Curriculum_mant/rellenar_software');
        r_s.then(function (successResponse) {
            $scope.rellenar_soft = successResponse.data.rellenar_s;
            $scope.carga_r_s = false;
            $scope.dif_r_s = true;
        },
        function (errorResponse) {
            $scope.carga_r_s = false;
            $scope.error_r_s = false;
            console.log("error rellena softwares");
        });
    };

    $scope.rellenar_grado_softwares = function () {
        $scope.carga_r_gs = true;
        $scope.error_r_gs = true;
        $scope.dif_r_gs = false;
        var r_gs = mantenedor_total.obtener_datos('/Curriculum_mant/rellenar_grado_software');
        r_gs.then(function (successResponse) {
            $scope.rellenar_g_soft = successResponse.data.rellenar_g_s;
            $scope.carga_r_gs = false;
            $scope.dif_r_gs = true;
        },
        function (errorResponse) {
            $scope.carga_r_gs = false;
            $scope.error_r_gs = false;
            console.log("error rellena grado softwares");
        });
    };

    $scope.rellenar_instituciones = function () {
        $scope.carga_r_in = true;
        $scope.error_r_in = true;
        $scope.dif_r_in = false;
        var r_in = mantenedor_total.obtener_datos('/Curriculum_mant/rellenar_institucion');
        r_in.then(function (successResponse) {
            $scope.rellenar_insti = successResponse.data.rellenar_ints;
            $scope.carga_r_in = false;
            $scope.dif_r_in = true;
        },
        function (errorResponse) {
            $scope.carga_r_in = false;
            $scope.error_r_in = false;
            console.log("error rellena instituciones");
        });
    };

    $scope.rellenar_capacitacion = function () {
        $scope.carga_r_c = true;
        $scope.error_r_c = true;
        $scope.dif_r_c = false;
        var r_c = mantenedor_total.obtener_datos('/Curriculum_mant/rellenar_capacitacion');
        r_c.then(function (successResponse) {
            $scope.rellenar_capa = successResponse.data.rellenar_cap;
            $scope.carga_r_c = false;
            $scope.dif_r_c = true;
        },
        function (errorResponse) {
            $scope.carga_r_c = false;
            $scope.error_r_c = false;
            console.log("error rellena capacitacion");
        });
    };

    $scope.rellenar_estado_capacitacion = function () {
        $scope.carga_r_ec = true;
        $scope.error_r_ec = true;
        $scope.dif_r_ec = false;
        var r_ec = mantenedor_total.obtener_datos('/Curriculum_mant/rellenar_estado_capacitacion');
        r_ec.then(function (successResponse) {
            $scope.rellenar_e_capa = successResponse.data.rellenar_e_cap;
            $scope.carga_r_ec = false;
            $scope.dif_r_ec = true;
        },
         function (errorResponse) {
             $scope.carga_r_ec = false;
             $scope.error_r_ec = false;
             console.log("error rellena estado capacitacion");
         });
    };

    $scope.rellenar_estudios = function () {
        $scope.carga_r_es = true;
        $scope.error_r_es = true;
        $scope.dif_r_es = false;
        var r_es = mantenedor_total.obtener_datos('/Curriculum_mant/rellenar_estudios');
        r_es.then(function (successResponse) {
            $scope.rellenar_es = successResponse.data.rellenar_est;
            $scope.carga_r_es = false;
            $scope.dif_r_es = true;
        },
        function (errorResponse) {
            $scope.carga_r_es = false;
            $scope.error_r_es = false;
            console.log("error rellena estudios");
        });
    };

    $scope.rellenar_tipo_estudios = function () {
        $scope.carga_r_tes = true;
        $scope.error_r_tes = true;
        $scope.dif_r_tes = false;
        var r_tes = mantenedor_total.obtener_datos('/Curriculum_mant/rellenar_tipo_estudios');
        r_tes.then(function (successResponse) {
            $scope.rellenar_t_es = successResponse.data.rellenar_t_est;
            $scope.carga_r_tes = false;
            $scope.dif_r_tes = true;
        },
         function (errorResponse) {
             $scope.carga_r_tes = false;
             $scope.error_r_tes = false;
             console.log("error rellena tipo estudios");
         });
    };

    $scope.rellenar_estado_estudios = function () {
        $scope.carga_r_ees = true;
        $scope.error_r_ees = true;
        $scope.dif_r_ees = false;
        var r_ees = mantenedor_total.obtener_datos('/Curriculum_mant/rellenar_estado_estudios');
        r_ees.then(function (successResponse) {
            $scope.rellenar_e_es = successResponse.data.rellenar_e_est;
            $scope.carga_r_ees = false;
            $scope.dif_r_ees = true;
        },
        function (errorResponse) {
            $scope.carga_r_ees = false;
            $scope.error_r_ees = false;
            console.log("error rellena estado estudios");
        });
    };

    $scope.rellenar_cargo_experiencias = function () {
        $scope.carga_r_cex = true;
        $scope.error_r_cex = true;
        $scope.dif_r_cex = false;
        var r_cex = mantenedor_total.obtener_datos('/Curriculum_mant/rellenar_cargo_exp');
        r_cex.then(function (successResponse) {
            $scope.rellenar_cargo_exp = successResponse.data.rellenar_c_exp;
            $scope.carga_r_cex = false;
            $scope.dif_r_cex = true;
        },
        function (errorResponse) {
            $scope.carga_r_cex = false;
            $scope.error_r_cex = false;
            console.log("error rellena cargo experiencias");
        });
    };

    $scope.rellenar_area_experiencias = function () {
        $scope.carga_r_aex = true;
        $scope.error_r_aex = true;
        $scope.dif_r_aex = false;
        var r_aex = mantenedor_total.obtener_datos('/Curriculum_mant/rellenar_area_exp');
        r_aex.then(function (successResponse) {
            $scope.rellenar_area_exp = successResponse.data.rellenar_a_exp;
            $scope.carga_r_aex = false;
            $scope.dif_r_aex = true;
        },
         function (errorResponse) {
             $scope.carga_r_aex = false;
             $scope.error_r_aex = false;
             console.log("error rellena area experiencias");
         });
    };

    $scope.rellenar_genero = function () {
        $scope.carga_r_ge = true;
        $scope.error_r_ge = true;
        $scope.dif_r_ge = false;
        var r_ge = mantenedor_total.obtener_datos('/Curriculum_mant/rellenar_genero');
        r_ge.then(function (successResponse) {
            $scope.rellenar_ge = successResponse.data.rellenar_g;
            $scope.carga_r_ge = false;
            $scope.dif_r_ge = true;
        },
        function (errorResponse) {
            $scope.carga_r_ge = false;
            $scope.error_r_ge = false;
            console.log("error rellena genero");
        });
    };

    /*$scope.rellenar_discapacidad = function () {
        $scope.carga_r_di = true;
        $scope.error_r_di = true;
        $scope.dif_r_di = false;
        var r_di = mantenedor_total.obtener_datos('/Curriculum_mant/rellenar_discapacidad');
        r_di.then(function (successResponse) {
                $scope.rellenar_dis = successResponse.data.rellenar_d;
                $scope.carga_r_di = false;
                $scope.dif_r_di = true;
        },
        function (errorResponse) {
            $scope.carga_r_di = false;
            $scope.error_r_di = false;
            console.log("error rellena discapacidad");
        });
    };*/


    $scope.descripcion_curriculum = function () {
        $scope.carga_desc = true;
        $scope.error_desc = false;
        $scope.dif_desc = false;
        var d_c = mantenedor_total.obtener_datos('/Curriculum_mant/vista_descripcion_curriculum');
        d_c.then(function (successResponse) {
            $scope.desc_curriculum = successResponse.data.desc_c;
            $scope.carga_desc = false;
            $scope.dif_desc = true;
        },
        function (errorResponse) {
            $scope.carga_desc = false;
            $scope.error_desc = true;
            console.log("error descripcion curriculum");
        });
    };

    $scope.experiencias_laborales_curriculum = function () {

        $scope.ver_exp = true;
        $scope.error_exp = false;
        $http.get('/Curriculum_mant/vista_experiencias_laborales_curriculum').then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                $scope.ver_exp = false;
                $scope.exp_curriculum = successResponse.data.exp_c;

            } else {
                $scope.exp_curriculum = "";
                $scope.ver_exp = false;
                console.log("exp false");
            }
        },
        function (errorResponse) {
            $scope.ver_exp = false;
            $scope.error_exp = true;


            console.log("exp error");
        });
    };

    $scope.profesion_curriculum = function () {

        $scope.ver_prof = true;
        $scope.error_prof = false;
        $http.get('/Curriculum_mant/vista_profesion_curriculum').then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log("lol2");
                $scope.ver_prof = false;
                $scope.prof_curriculum = successResponse.data.profesion_c;

            } else {
                $scope.ver_prof = false;
                $scope.prof_curriculum = "";

                console.log("bla bla bla");
            }
        },
        function (errorResponse) {
            $scope.ver_prof = false;
            $scope.error_prof = true;
            console.log("error my friend2");
        });
    };

    $scope.estudios_curriculum = function () {

        $scope.ver_est = true;
        $scope.error_est = false;
        $http.get('/Curriculum_mant/vista_estudios_curriculum').then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log("lol2");
                $scope.ver_est = false;
                $scope.est_curriculum = successResponse.data.estudios_c;

            } else {
                $scope.ver_est = false;
                $scope.est_curriculum = "";

                console.log("bla bla bla");
            }
        },
        function (errorResponse) {
            $scope.ver_est = false;
            $scope.error_est = true;
            console.log("error my friend2");
        });
    };
    $scope.capacitaciones_curriculum = function () {

        $scope.ver_cap = true;
        $scope.error_cap = false;
        $http.get('/Curriculum_mant/vista_capacitaciones_curriculum').then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log("lol2");
                $scope.ver_cap = false;
                $scope.cap_curriculum = successResponse.data.capacitaciones_c;

            } else {
                $scope.ver_cap = false;
                $scope.cap_curriculum = "";

                console.log("bla bla bla");
            }
        },
        function (errorResponse) {
            $scope.ver_cap = false;
            $scope.error_cap = true;
            console.log("error my friend2");
        });
    };
    $scope.softwares_curriculum = function () {

        $scope.ver_soft = true;
        $scope.error_soft = false;
        $http.get('/Curriculum_mant/vista_softwares_curriculum').then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log("lol2");
                $scope.ver_soft = false;
                $scope.soft_curriculum = successResponse.data.softwares_c;

            } else {
                $scope.ver_soft = false;
                $scope.soft_curriculum = "";

                console.log("bla bla bla");
            }
        },
        function (errorResponse) {
            $scope.ver_soft = false;
            $scope.error_soft = true;
            console.log("error my friend2");
        });
    };
    $scope.habilidades_curriculum = function () {

        $scope.ver_hab = true;
        $scope.error_hab = false;
        $http.get('/Curriculum_mant/vista_habilidades_curriculum').then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log("lol2");
                $scope.ver_hab = false;
                $scope.hab_curriculum = successResponse.data.habilidades_c;

            } else {
                $scope.ver_hab = false;
                $scope.hab_curriculum = "";

                console.log("bla bla bla");
            }
        },
        function (errorResponse) {
            $scope.ver_hab = false;
            $scope.error_hab = true;
            console.log("error my friend2");
        });
    };
    $scope.idiomas_curriculum = function () {

        $scope.ver_idioma = true;
        $scope.error_idioma = false;
        $http.get('/Curriculum_mant/vista_idiomas_curriculum').then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log("lol2");
                $scope.ver_idioma = false;
                $scope.idioma_curriculum = successResponse.data.idiomas_c;

            } else {
                $scope.ver_idioma = false;
                $scope.idioma_curriculum = "";

                console.log("bla bla bla");
            }
        },
        function (errorResponse) {
            $scope.ver_idioma = false;
            $scope.error_idioma = true;
            console.log("error my friend2");
        });
    };
    $scope.documentos_curriculum = function () {

        $scope.ver_doc = true;
        $scope.error_doc = false;
        $http.get('/Curriculum_mant/vista_documento_curriculum').then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log("lol2");
                $scope.ver_doc = false;
                $scope.doc_curriculum = successResponse.data.doc_c;

            } else {
                $scope.ver_doc = false;
                $scope.doc_curriculum = "";

                console.log("bla bla bla");
            }
        },
        function (errorResponse) {
            $scope.ver_doc = false;
            $scope.error_doc = true;
            console.log("error my friend2");
        });
    };
}]);



app.controller('data1', ['$scope', '$http', '$filter', '$mdDialog', 'mantenedor_total', function ($scope, $http, $filter, $mdDialog, mantenedor_total) {

    $scope.respuesta_cuestionario_multiple = [];
    $scope.datos_oferta = {};
    $scope.newEmployee1 = [];
    $scope.newEmployee2 = [];


    $scope.abremodal = function (url, id) {
        mantenedor_total.modal(url, id, $scope);
    };


    $scope.addEmployee = function () {

        var respuestas_cuestionario = { 'respuesta_cuestionario_multiple': $scope.respuesta_cuestionario_multiple, 'datos_oferta': $scope.datos_oferta };
        var add_emp = mantenedor_total.agregar_datos('/Ofertas_Control/create_postulacion', respuestas_cuestionario);
        add_emp.then(function (successResponse) {
            $scope.newEmployee2 = successResponse.data.employee;
            window.location.reload();
        },
        function (errorResponse) {
            console.log("error añadir empleado")
        });
    };


    $scope.agregar_idioma = function () {
        var add_idioma = { 'nuevo_idioma': $scope.agrega_idioma };
        var add_emp = mantenedor_total.agregar_datos('/Curriculum_mant/agregar_idiomas_curriculum', add_idioma);
        add_emp.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.idiomas_curriculum();
        },
        function (errorResponse) {
            console.log('error agregar idioma');
        });
    };

    $scope.eliminar_idioma = function () {
        var id_idioma = $scope.elimina_idioma.id;
        var del_idi = mantenedor_total.borrar_datos('/Curriculum_mant/eliminar_idiomas_curriculum', id_idioma);
        del_idi.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.idiomas_curriculum();
        },
        function (errorResponse) {
            console.log('error eliminar idioma');
        });
    };

    $scope.agregar_habilidad = function () {
        var add_habilidad = { 'nueva_habilidad': $scope.agrega_habilidad };
        var add_hab = mantenedor_total.agregar_datos('/Curriculum_mant/agregar_habilidades_curriculum', add_habilidad);
        add_hab.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.habilidades_curriculum();
        },
        function (errorResponse) {
            console.log('error agregar habilidad');
        });
    };

    $scope.eliminar_habilidad = function () {

        var id_habilidad = $scope.elimina_habilidad.id;
        var del_hab = mantenedor_total.borrar_datos('/Curriculum_mant/eliminar_habilidades_curriculum/', id_habilidad);
        del_hab.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.habilidades_curriculum();
        },
        function (errorResponse) {
            console.log('error eliminar habilidad');
        });
    };

    $scope.agregar_software = function () {
        console.log($scope.agrega_software);
        var add_software = { 'nuevo_software': $scope.agrega_software };
        var add_soft = mantenedor_total.agregar_datos('/Curriculum_mant/agregar_softwares_curriculum', add_software);
        add_soft.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.softwares_curriculum();
        },
        function (errorResponse) {
            console.log('error agrega software');
        });
    };

    $scope.eliminar_software = function () {
        var id_software = $scope.elimina_software.id;
        var del_soft = mantenedor_total.borrar_datos('/Curriculum_mant/eliminar_softwares_curriculum/', id_software);
        del_soft.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.softwares_curriculum();
        },
        function (errorResponse) {

            console.log('error elimina software');
        });
    };

    $scope.agregar_profesion = function () {
        var add_profesion = { 'nueva_profesion': $scope.agrega_profesion };
        var add_prof = mantenedor_total.agregar_datos('/Curriculum_mant/agregar_profesiones_curriculum', add_profesion);
        add_prof.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.profesion_curriculum();
        },
        function (errorResponse) {
            console.log('error agregar profesion');
        });
    };

    $scope.eliminar_profesion = function () {
        var id_profesion = $scope.elimina_profesion.id;
        var del_prof = mantenedor_total.borrar_datos('/Curriculum_mant/eliminar_profesiones_curriculum/', id_profesion);
        del_prof.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.profesion_curriculum();
        },
        function (errorResponse) {
            console.log('error elimina profesion');
        });
    };

    $scope.agregar_experiencia_laboral = function () {
        var add_experiencia_laboral = { 'nueva_experiencia_laboral': $scope.agrega_experiencia_laboral };
        var add_exp = mantenedor_total.agregar_datos('/Curriculum_mant/agregar_experiencias_laborales_curriculum', add_experiencia_laboral);
        add_exp.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.experiencias_laborales_curriculum();
        },
        function (errorResponse) {
            console.log('error agregar experiencia laboral');
        });
    };

    $scope.eliminar_experiencia_laboral = function () {
        var id_experiencia_laboral = $scope.elimina_experiencia_laboral.id;
        var del_exp = mantenedor_total.borrar_datos('/Curriculum_mant/eliminar_experiencias_laborales_curriculum/', id_experiencia_laboral);
        del_exp.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.experiencias_laborales_curriculum();
        },
        function (errorResponse) {
            console.log('error eliminar experiencia laboral');
        });
    };

    $scope.agregar_referencia_laboral = function () {
        var add_referencia_laboral = { 'nueva_referencia_laboral': $scope.agrega_referencia_laboral };
        var add_ref = mantenedor_total.agregar_datos('/Curriculum_mant/agregar_referencias_laborales_curriculum', add_referencia_laboral);
        add_ref.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.experiencias_laborales_curriculum();
        },
        function (errorResponse) {
            console.log('error agregar referencia laboral');
        });
    };

    $scope.eliminar_referencia_laboral = function () {
        var id_experiencia_laboral = $scope.elimina_referencia_laboral.id;
        var del_ref = mantenedor_total.borrar_datos('/Curriculum_mant/eliminar_referencias_laborales_curriculum/', id_experiencia_laboral);
        del_ref.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.experiencias_laborales_curriculum();
        },
        function (errorResponse) {
            console.log('error eliminar referencia laboral');
        });
    };

    $scope.agregar_estudio = function () {
        var add_estudio = { 'nuevo_estudio': $scope.agrega_estudio };
        var add_est = mantenedor_total.agregar_datos('/Curriculum_mant/agregar_estudios_curriculum', add_estudio);
        add_est.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.estudios_curriculum();
        },
        function (errorResponse) {
            console.log('error agrega estudio');
        });
    };

    $scope.eliminar_estudio = function () {
        var id_estudio = $scope.elimina_estudio.id;
        var del_est = mantenedor_total.borrar_datos('/Curriculum_mant/eliminar_estudios_curriculum/', id_estudio);
        del_est.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.estudios_curriculum();
        },
        function (errorResponse) {
            console.log('error eliminar estudios');
        });
    };

    $scope.agregar_documento = function () {
        var add_documento = { 'nuevo_documento': $scope.agrega_documento };
        var add_doc = mantenedor_total.agregar_datos('/Curriculum_mant/agregar_documentos_curriculum', add_documento);
        add_doc.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.documentos_curriculum();
        },
        function (errorResponse) {
            console.log('error agregar documento');
        });
    };

    $scope.eliminar_documento = function () {
        var id_documento = $scope.elimina_documento.id;
        var del_doc = mantenedor_total.borrar_datos('/Curriculum_mant/eliminar_documentos_curriculum/', id_documento);
        del_doc.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.documentos_curriculum();
        },
        function (errorResponse) {
            console.log('error eliminar documento');
        });
    };

    $scope.agregar_capacitacion = function () {
        var add_capacitacion = { 'nueva_capacitacion': $scope.agrega_capacitacion };
        var add_cap = mantenedor_total.agregar_datos('/Curriculum_mant/agregar_capacitaciones_curriculum', add_capacitacion);
        add_cap.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.capacitaciones_curriculum();
        },
        function (errorResponse) {
            console.log('error agregar capacitacion');
        });
    };

    $scope.eliminar_capacitacion = function () {
        var id_capacitacion = $scope.elimina_capacitacion.id;
        var del_cap = mantenedor_total.borrar_datos('/Curriculum_mant/eliminar_capacitaciones_curriculum/', id_capacitacion);
        del_cap.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.capacitaciones_curriculum();
        },
         function (errorResponse) {
             console.log('error eliminar capacitacion');
         });
    };

    $scope.crear_cuenta_persona = function () {
        var add_cuenta = { 'datos_cuenta_personas': $scope.datos_cuenta_persona };
        var add_cuen = mantenedor_total.agregar_datos('/cuenta/agregar_cuenta_persona', add_cuenta);
        add_cuen.then(function (successResponse) {
            if (successResponse.data.success == true) {
                $scope.respuesta = successResponse.data.respuesta;
                $scope.closeDialog();
            }
            else {
                console.log("error no agrego ");
            }
        },
        function (errorResponse) {
            console.log('error crear cuenta persona');
        });
    };

    $scope.recordar_cuenta_persona = function () {
        console.log($scope.recordar_cuenta_personas);
        //var rut = $scope.recordar_cuenta_personas.identificacion_persona;
        var rec_cuenta = { 'recordar_cuenta_personas': $scope.recordar_cuenta_personas };
        var rec_cuen = mantenedor_total.envia_correo(0, rec_cuenta);
        debugger;
        rec_cuen.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();

        },
        function (errorResponse) {
            console.log('error recordar cuenta');
        });
    };

    $scope.actualizar_pass = function () {
        console.log($scope.actualiza_pass);

        var add_pass = { 'actualiza_pass': $scope.actualiza_pass };

        $http.post('/cuenta/actualiza_pass', add_pass).then(
        function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;

        },
        function (errorResponse) {
            // handle errors here
            console.log('error actualiza_pass');
        });
    };
    $scope.desactivar_cuenta = function () {


        $http.post('/cuenta/desactiva_cuenta').then(
        function (successResponse) {
            console.log('success3');

            //$scope.abremodal("../Curriculum_mant/agregar_vista_habilidades_curriculum/", '');
            window.location = "../login";

        },
        function (errorResponse) {
            // handle errors here
            console.log(':C');
        });
    };

    $scope.actualiza_descripcion = function () {
        console.log($scope.actualizar_descripcion);

        var add_desc = { 'actualizar_descripcion': $scope.actualizar_descripcion };

        $http.post('/Curriculum_mant/actualiza_descripcion', add_desc).then(
        function (successResponse) {

            if (successResponse.data.success == true) {


                $scope.respuesta = successResponse.data.respuesta;
                $scope.closeDialog();
                $scope.descripcion_curriculum();

            } else {

                console.log("bla bla bla");
            }
        }

        ,
        function (errorResponse) {

            console.log('error actualizar descripcion');
        });


    };

    $scope.rellenar_ciudad = function (id) {
        $scope.carga_r_reg = true;
        $scope.error_r_reg = true;
        $scope.dif_r_reg = false;
        var r_reg = mantenedor_total.obtener_datos('/faq/rellenar_ciudades/' + id);
        r_reg.then(function (successResponse) {
            $scope.ciudades = successResponse.data.ciudades;
            $scope.carga_r_reg = false;
            $scope.dif_r_reg = true;
        },
        function (errorResponse) {
            $scope.carga_r_reg = false;
            $scope.error_r_reg = false;
            console.log("error rellena ciudad");
        });
    };

    $scope.rellenar_comuna = function (id) {
        $scope.carga_r_ci = true;
        $scope.error_r_ci = true;
        $scope.dif_r_ci = false;
        var r_ci = mantenedor_total.obtener_datos('/faq/rellenar_comunas/' + id);
        r_ci.then(function (successResponse) {
            $scope.comunas = successResponse.data.comunas;
            $scope.carga_r_ci = false;
            $scope.dif_r_ci = true
        },
        function (errorResponse) {
            $scope.carga_r_ci = false;
            $scope.error_r_ci = false;
            console.log("error rellena comuna");
        });
    };

    $scope.listar_datos_personas = function () {

        $http.get('/Curriculum_mant/datos_persona_curriculum').then(
        function (successResponse) {
            if (successResponse.data.success == true) {

                // var record = successResponse.data.datos_p;
                $scope.update_p = successResponse.data.datos_p;

                $scope.date = $scope.update_p.fecha_nacimiento_persona.replace('/Date(', '').replace(')/', '');
                $scope.update_p.fecha_nacimiento_persona = $filter('date')($scope.date, 'yyyy-MM-dd');

                $scope.rellenar_ciudad($scope.update_p.id_region);
                $scope.rellenar_comuna($scope.update_p.id_ciudad);
            }
            else {

            }
        },
        function (errorResponse) {
            $scope.error_datos = true;
            $scope.ver_datos = false;
            console.log("error listar datos persona");
        });
    };


    $scope.detalle_experiencia_laboral = function (a) {

        $http.get('/Curriculum_mant/detalle_experiencias_laborales_curriculum/' + a).then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log('success');

                var record2 = successResponse.data.exp_c;

                $scope.update_nombre_experiencia_laboral = record2.nombre_experiencia_laboral;
                $scope.update_empresa_experiencia_laboral = record2.empresa_experiencia_laboral;

                $scope.update_id_cargo_experiencia_laboral = record2.id_cargo_experiencia_laboral;

                $scope.fecha_inicio = record2.ano_inicio_experiencia_laboral.replace('/Date(', '').replace(')/', '');
                $scope.update_ano_inicio_experiencia_laboral = $filter('date')($scope.fecha_inicio, 'dd/MM/yyyy');

                $scope.fecha_termino = record2.ano_termino_experiencia_laboral.replace('/Date(', '').replace(')/', '');
                $scope.update_ano_termino_experiencia_laboral = $filter('date')($scope.fecha_termino, 'dd/MM/yyyy');

                $scope.update_id_area_experiencia_laboral = record2.id_area_experiencia_laboral;

                $scope.update_detalle_experiencia_laboral = record2.detalle_experiencia_laboral;


            }
            else {

            }
        },
        function (errorResponse) {

            console.log("error detalle experiencia laboral");
        });
    };
    $scope.detalle_estudios = function (a) {

        $http.get('/Curriculum_mant/detalle_estudios_curriculum/' + a).then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log('success');

                var record3 = successResponse.data.estudios_c;
                $scope.update_id_estudio = record3.id_estudio;
                $scope.update_id_tipo_estudio = record3.id_tipo_estudio;
                $scope.update_id_estado_estudio = record3.id_estado_estudio;
                $scope.update_id_institucion = record3.id_institucion;
                $scope.fecha_inicio = record3.ano_inicio_estudio_curriculum.replace('/Date(', '').replace(')/', '');
                $scope.update_ano_inicio_estudio_curriculum = $filter('date')($scope.fecha_inicio, 'dd-MM-yyyy');
                $scope.fecha_termino = record3.ano_termino_estudio_curriculum.replace('/Date(', '').replace(')/', '');
                $scope.update_ano_termino_estudio_curriculum = $filter('date')($scope.fecha_termino, 'dd-MM-yyyy');


            }
            else {

            }
        },
        function (errorResponse) {

            console.log("error detalle estudios");
        });
    };
    $scope.detalle_capacitaciones = function (a) {
        var d_c = mantenedor_total.obtener_datos('/Curriculum_mant/detalle_capacitaciones_curriculum/' + a);
        d_c.then(function (successResponse) {

            var record2 = successResponse.data.ca_c;
            $scope.update_descripcion_capacitacion = record2.descripcion_capacitacion;
            $scope.update_id_capacitacion = record2.id_capacitacion;
            $scope.fecha_inicio = record2.ano_inicio_capacitacion_curriculum.replace('/Date(', '').replace(')/', '');
            $scope.update_ano_inicio_capacitacion_curriculum = $filter('date')($scope.fecha_inicio, 'dd-MM-yyyy');
            $scope.fecha_termino = record2.ano_termino_capacitacion_curriculum.replace('/Date(', '').replace(')/', '');
            $scope.update_ano_termino_capacitacion_curriculum = $filter('date')($scope.fecha_termino, 'dd-MM-yyyy');
            $scope.update_id_estado_capacitacion = record2.id_estado_capacitacion;
            $scope.update_horas_capacitacion = record2.horas_capacitacion;
            $scope.update_id_tipo_capacitacion = record2.id_tipo_capacitacion;
            $scope.update_id_institucion = record2.id_institucion;
        },
        function (errorResponse) {
            console.log("error detalle capacitaciones");
        });
    };


    $scope.actualizar_datos_persona = function (id) {


        var actualiza_persona = {

            nombre_persona: $scope.update_p.nombre_persona,
            apellido_paterno_persona: $scope.update_p.apellido_paterno_persona,
            apellido_materno_persona: $scope.update_p.apellido_materno_persona,
            id_tipo_identificacion_persona: $scope.update_p.id_tipo_identificacion_persona,
            fecha_nacimiento_persona: $scope.update_p.fecha_nacimiento_persona,
            identificacion_persona: $scope.update_p.identificacion_persona,
            direccion_curriculum: $scope.update_p.direccion_curriculum,
            id_tipo_persona: $scope.update_p.id_tipo_persona,
            sueldo_esperado: $scope.update_p.sueldo_esperado,
            correo_electronico_persona: $scope.update_p.correo_electronico_persona,
            telefono_curriculum_1: $scope.update_p.telefono_curriculum_1,
            telefono_curriculum_2: $scope.update_p.telefono_curriculum_2,
            id_comuna: $scope.update_p.id_comuna

        };
        var edit_per = { 'datos_per': actualiza_persona };
        var upd_per = mantenedor_total.actualizar_datos('/Curriculum_mant/actualiza_datos_persona_curriculum/', id, edit_per);
        upd_per.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.datos_persona();
        }
    )
    };

    $scope.editar_experiencia_laboral = function (id) {
        var edita_experiencia_laboral = {
            nombre_experiencia_laboral: $scope.update_nombre_experiencia_laboral,
            empresa_experiencia_laboral: $scope.update_empresa_experiencia_laboral,
            id_cargo_experiencia_laboral: $scope.update_id_cargo_experiencia_laboral,
            ano_inicio_experiencia_laboral: $scope.update_ano_inicio_experiencia_laboral,
            ano_termino_experiencia_laboral: $scope.update_ano_termino_experiencia_laboral,
            id_area_experiencia_laboral: $scope.update_id_area_experiencia_laboral,
            detalle_experiencia_laboral: $scope.update_detalle_experiencia_laboral
        };
        var edit_exp = { 'edita_experiencia_laboral': edita_experiencia_laboral };
        var upd_exp = mantenedor_total.actualizar_datos('/Curriculum_mant/edita_experiencia_laboral/', id, edit_exp);
        upd_exp.then(function (successResponse) {
            $scope.respuesta = successResponse.data.respuesta;
            $scope.closeDialog();
            $scope.experiencias_laborales_curriculum();
        }
    )
    };


    $scope.editar_estudios = function (a) {

        var edita_estudios = {


            id_estudio: $scope.update_id_estudio,
            id_tipo_estudio: $scope.update_id_tipo_estudio,
            id_estado_estudio: $scope.update_id_estado_estudio,
            id_institucion: $scope.update_id_institucion,
            ano_inicio_estudio_curriculum: $scope.update_ano_inicio_estudio_curriculum,
            ano_termino_estudio_curriculum: $scope.update_ano_termino_estudio_curriculum
        };

        var edit_es = { 'edita_estudios': edita_estudios };
        var upd_est = mantenedor_total.actualizar_datos('/Curriculum_mant/edita_estudios/', a, edit_es);
        upd_est.then(function (successResponse) {

            if (successResponse.data.success == true) {

                $scope.respuesta = successResponse.data.respuesta;
                $scope.closeDialog();
                $scope.estudios_curriculum();

            } else {

                console.log("false editar estudios ");
            }
        }

        ,
        function (errorResponse) {

            console.log('error editar estudios');
        });


    };
    $scope.editar_capacitacion = function (a) {

        var edita_capacitacion = {
            descripcion_capacitacion: $scope.update_descripcion_capacitacion,
            id_capacitacion: $scope.update_id_capacitacion,
            ano_inicio_capacitacion_curriculum: $scope.update_ano_inicio_capacitacion_curriculum,
            ano_termino_capacitacion_curriculum: $scope.update_ano_termino_capacitacion_curriculum,
            id_estado_capacitacion: $scope.update_id_estado_capacitacion,
            horas_capacitacion: $scope.update_horas_capacitacion,
            id_tipo_capacitacion: $scope.update_id_tipo_capacitacion,
            id_institucion: $scope.update_id_institucion
        };
        var edit_cap = { 'edita_capacitacion': edita_capacitacion };
        $http.post('/Curriculum_mant/edita_capacitaciones/' + a, edit_cap).then(
        function (successResponse) {

            if (successResponse.data.success == true) {
                console.log('success3');

                $scope.respuesta = successResponse.data.respuesta;
                $scope.closeDialog();
                $scope.capacitaciones_curriculum();


            } else {

                console.log("bla bla bla");
            }
        }

        ,
        function (errorResponse) {

            console.log('error capacitaciones curriculum');
        });


    };
    $scope.actualizar_foto = function (a) {
        var up_foto = new FormData();
        up_foto.append('id_foto', a);
        up_foto.append('file', $scope.actualiza_fotos.fotos);
        $http.post('/curriculum_mant/actualiza_foto_curriculum', up_foto
        , {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }
        ).then(
        function (successResponse) {

            $scope.datos_per = successResponse.data.datos_p;
            $scope.closeDialog();
            $scope.foto_persona();
        },
        function (errorResponse) {

            console.log("error actualizar foto");
        });
    };



}]);


app.controller('control_detalle_ofertas', ['$scope', '$http', function ($scope, $http) {

    $scope.ver_detalle_oferta = {};
    $scope.habilidad_oferta = {};
    $scope.idiomas_oferta = {};
    $scope.profesiones_oferta = {};
    $scope.softwares_oferta = {};
    $scope.cuestionario_oferta = {};

    $scope.vista_datos_detalle_oferta = function (id_oferta) {

        $scope.ver_det_of = true;
        $scope.error_det_of = false;
        $http.get('/Ofertas_Control/vista_datos_detalle_oferta/' + id_oferta).then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                $scope.ver_det_of = false;
                $scope.ver_detalle_oferta = successResponse.data.det_of_c;


            } else {
                $scope.ver_detalle_oferta = "";
                $scope.ver_det_of = false;
                console.log("gg");
            }
        },
        function (errorResponse) {
            $scope.ver_det_of = false;
            $scope.error_det_of = true;


            console.log("error my friend");
        });
    };
    $scope.vista_habilidades_oferta = function (id_oferta) {

        $scope.ver_hab_of = true;
        $scope.error_hab_of = false;
        $http.get('/Ofertas_Control/vista_habilidades_oferta/' + id_oferta).then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log("izi2");
                $scope.ver_hab_of = false;
                $scope.habilidad_oferta = successResponse.data.habilidad_o;


            } else {
                $scope.habilidad_oferta = "";
                $scope.ver_hab_of = false;
                console.log("gg");
            }
        },
        function (errorResponse) {
            $scope.ver_hab_of = false;
            $scope.error_hab_of = true;


            console.log("error my friend");
        });
    };

    $scope.vista_idiomas_oferta = function (id_oferta) {

        $scope.ver_idioma_of = true;
        $scope.error_idioma_of = false;
        $http.get('/Ofertas_Control/vista_idiomas_oferta/' + id_oferta).then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log("izi2");
                $scope.ver_idioma_of = false;
                $scope.idiomas_oferta = successResponse.data.idiomas_o;


            } else {
                $scope.idiomas_oferta = "";
                $scope.ver_idioma_of = false;
                console.log("gg");
            }
        },
        function (errorResponse) {
            $scope.ver_idioma_of = false;
            $scope.error_idioma_of = true;


            console.log("error my friend");
        });
    };

    $scope.vista_documentos_oferta = function (id_oferta) {

        $scope.ver_doc_of = true;
        $scope.error_doc_of = false;
        $http.get('/Ofertas_Control/vista_documentos_oferta/' + id_oferta).then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log("izi2");
                $scope.ver_doc_of = false;
                $scope.doc_oferta = successResponse.data.doc_o;


            } else {
                $scope.doc_oferta = "";
                $scope.ver_doc_of = false;
                console.log("gg");
            }
        },
        function (errorResponse) {
            $scope.ver_doc_of = false;
            $scope.error_doc_of = true;


            console.log("error my friend");
        });
    };
    $scope.vista_categorias_oferta = function (id_oferta) {

        $scope.ver_cat_of = true;
        $scope.error_cat_of = false;
        $http.get('/Ofertas_Control/vista_categorias_oferta/' + id_oferta).then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log("izi2");
                $scope.ver_cat_of = false;
                $scope.cat_oferta = successResponse.data.cat_o;


            } else {
                $scope.cat_oferta = "";
                $scope.ver_cat_of = false;
                console.log("gg");
            }
        },
        function (errorResponse) {
            $scope.ver_cat_of = false;
            $scope.error_cat_of = true;


            console.log("error my friend");
        });
    };
    $scope.vista_profesiones_oferta = function (id_oferta) {

        $scope.ver_prof_of = true;
        $scope.error_prof_of = false;
        $http.get('/Ofertas_Control/vista_profesiones_oferta/' + id_oferta).then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log("izi2");
                $scope.ver_prof_of = false;
                $scope.profesiones_oferta = successResponse.data.prof_o;


            } else {
                $scope.profesiones_oferta = "";
                $scope.ver_prof_of = false;
                console.log("gg");
            }
        },
        function (errorResponse) {
            $scope.ver_prof_of = false;
            $scope.error_prof_of = true;


            console.log("error my friend");
        });
    };
    $scope.vista_softwares_oferta = function (id_oferta) {

        $scope.ver_soft_of = true;
        $scope.error_soft_of = false;
        $http.get('/Ofertas_Control/vista_softwares_oferta/' + id_oferta).then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log("izi2");
                $scope.ver_soft_of = false;
                $scope.softwares_oferta = successResponse.data.soft_o;


            } else {
                $scope.softwares_oferta = "";
                $scope.ver_soft_of = false;
                console.log("gg");
            }
        },
        function (errorResponse) {
            $scope.ver_soft_of = false;
            $scope.error_soft_of = true;


            console.log("error my friend");
        });
    };




    $scope.vista_cuestionario_oferta = function (id_cuestionario) {

        $scope.ver_cues_of = true;
        $scope.error_cues_of = false;
        $http.get('/Ofertas_Control/vista_cuestionario_oferta/' + id_cuestionario).then(
        function (successResponse) {
            if (successResponse.data.success == true) {

                $scope.ver_cues_of = false;
                $scope.cuestionario_oferta = successResponse.data.cuestionario_o;
                console.log($scope.cuestionario_oferta);

            } else {
                $scope.cuestionario_oferta = "";
                $scope.ver_cues_of = false;
                console.log("gg");
            }
        },
        function (errorResponse) {
            $scope.ver_cues_of = false;
            $scope.error_cues_of = true;


            console.log("error my friend");
        });
    };
    $scope.vista_respuestas_multiple_pregunta = function (id_pregunta) {

        $scope.ver_resp_pre = true;
        $scope.error_resp_pre = false;
        $http.get('/Ofertas_Control/vista_respuestas_multiple_pregunta/' + id_pregunta).then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log("izi2");
                $scope.ver_resp_pre = false;
                $scope.respuesta_pregunta = successResponse.data.respuesta_p;


            } else {
                $scope.respuesta_pregunta = "";
                $scope.ver_resp_pre = false;
                console.log("gg");
            }
        },
        function (errorResponse) {
            $scope.ver_resp_pre = false;
            $scope.error_resp_pre = true;


            console.log("error my friend");
        });
    };


}]);

app.controller('faq_portal', ['$scope', '$http', function ($scope, $http) {

    $scope.preguntas_frecuentes = {};
    $scope.c_preguntas_frecuentes = {};
    $scope.p_f = function () {

        $scope.ver_p_f = true;
        $scope.ver_error_p_f = false;
        $http.get('/faq/listar_faq').then(
        function (successResponse) {
            if (successResponse.data.success == true) {

                $scope.ver_p_f = false;
                $scope.preguntas_frecuentes = successResponse.data.pregunta_f;
                console.log($scope.preguntas_frecuentes);

            } else {
                $scope.preguntas_frecuentes = "";
                $scope.ver_p_f_of = false;
                console.log("gg");
            }
        },
        function (errorResponse) {
            $scope.ver_p_f = false;
            $scope.error_p_f = true;


            console.log("error my friend");
        });
    };
    $scope.c_p_f = function () {

        $scope.ver_c_p_f = true;
        $scope.ver_error_c_p_f = false;
        $http.get('/faq/listar_categorias_faq').then(
        function (successResponse) {
            if (successResponse.data.success == true) {

                $scope.ver_c_p_f = false;
                $scope.c_preguntas_frecuentes = successResponse.data.pregunta_f_c;
                console.log($scope.c_preguntas_frecuentes);

            } else {
                $scope.c_preguntas_frecuentes = "";
                $scope.ver_c_p_f_of = false;
                console.log("gg");
            }
        },
        function (errorResponse) {
            $scope.ver_c_p_f = false;
            $scope.ver_error_c_p_f = true;


            console.log("error my friend");
        });
    };

}]);

app.controller('inicio_portal', ['$scope', '$http', 'mantenedor_total', function ($scope, $http, mantenedor_total) {

    $scope.p_i = function () {

        $scope.ver_p_i = true;
        $scope.ver_error_p_i = true;
        var mant = mantenedor_total.obtener_datos('/Curriculum_mant/postulaciones_inicio');
        mant.then(function (successResponse) {
            if (successResponse.data.success == true) {
                $scope.postulaciones_inicio = successResponse.data.postulaciones_i;
                $scope.ver_p_i = false;
            } else {
                $scope.ver_p_i = false;
            }
        },
         function (errorResponse) {
             $scope.ver_p_i = false;
             $scope.ver_error_p_i = false;
         });
    };

    $scope.p_o = function () {

        $scope.ver_p_o = true;
        $scope.ver_error_p_o = true;
        var mant = mantenedor_total.obtener_datos('/Curriculum_mant/postulaciones_inicio_ofertas');
        mant.then(function (successResponse) {
            if (successResponse.data.success == true) {
                $scope.postulaciones_inicio_ofertas = successResponse.data.postulaciones_o;
                $scope.ver_p_o = false;
            } else {
                $scope.ver_p_o = false;
            }
        },
        function (errorResponse) {
            $scope.ver_p_o = false;
            $scope.ver_error_p_o = false;
        });
    };
}]);

app.controller('postulaciones', ['$scope', '$http', function ($scope, $http) {

    $scope.listado_postulacion = {};
    $scope.r_e_post = {};

    $scope.rellenar_estado_postulacion = function () {


        $http.get('/Curriculum_mant/rellenar_estado_postulaciones').then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log('successwerwer');
                $scope.r_e_post = successResponse.data.rellenar_e_pos;
                console.log(successResponse.data.r_e_post);

            }
            else {
                $scope.r_e_post = null;

                console.log("bla bla blawerwer");
            }
        },
        function (errorResponse) {

            console.log("error my friendwerwer");
        });
    };
    $scope.rellenar_listado_postulacion = function () {


        $http.get('/Curriculum_mant/listar_postulaciones_persona').then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log('successwerwer');
                $scope.listado_postulacion = successResponse.data.listado_post;
                console.log(successResponse.data.listado_postulacion);

            }
            else {
                $scope.listado_postulacion = null;

                console.log("bla bla blawerwer");
            }
        },
        function (errorResponse) {

            console.log("error my friendwerwer");
        });
    };

}]);

app.controller('micuenta', ['$scope', '$http', function ($scope, $http) {

    $scope.datos_per = {};

    $scope.datos_persona = function () {

        $scope.ver_datos = true;
        $scope.error_datos = false;
        $http.get('/Curriculum_mant/datos_persona_curriculum').then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log('success');
                $scope.datos_per = successResponse.data.datos_p;
                console.log(successResponse.data.datos_p);
                $scope.ver_datos = false;
            }
            else {
                $scope.datos_per = null;
                $scope.ver_datos = false;
                console.log("bla bla bla");
            }
        },
        function (errorResponse) {
            $scope.error_datos = true;
            $scope.ver_datos = false;
            console.log("error my friend");
        });
    };

}]);

app.controller('soporte', ['$scope', '$http', 'mantenedor_total', function ($scope, $http, mantenedor_total) {



    $scope.abremodal = function (url, id) {
        mantenedor_total.modal(url, id, $scope);
    };

    $scope.soporte_men = function () {
        console.log($scope.soporte_datos);


        var nuevo_soporte = new FormData();
        nuevo_soporte.append('nombre_remitente', $scope.soporte_datos.nombre_remitente);
        nuevo_soporte.append('correo_remitente', $scope.soporte_datos.correo_remitente);
        nuevo_soporte.append('rut_remitente', $scope.soporte_datos.rut_remitente);
        nuevo_soporte.append('digito_rut_remitente', $scope.soporte_datos.digito_rut_remitente);
        nuevo_soporte.append('asunto_mensaje', $scope.soporte_datos.asunto_mensaje);
        nuevo_soporte.append('descripcion_mensaje', $scope.soporte_datos.descripcion_mensaje);
        nuevo_soporte.append('fotos', $scope.soporte_datos.fotos);

        $http.post('/soporte/enviar_soporte', nuevo_soporte
        , {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }
        ).then(
        function (successResponse) {
            if (successResponse.data.success == true) {
                console.log('success');
                $scope.datos_per = successResponse.data.datos_p;

            }
            else {

                console.log("bla bla bla");
            }
        },
        function (errorResponse) {

            console.log("error my friend");
        });
    };

}]);

app.directive('fileModel', ['$parse', function ($parse) {
    return {

        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);




app.controller('PopupCtrl', ['$scope', '$filter', '$http', 'mantenedor_total', function ($scope, $filter, $http, mantenedor_total) {


    var r_r = mantenedor_total.obtener_datos('/faq/rellenar_regiones');
    r_r.then(function (successResponse) {
        if (successResponse.data.success == true) {
            $scope.regiones = successResponse.data.regiones;



        }
        else {
            //$scope.load_datos = false;
        }
    },
    function (errorResponse) {
        //$scope.error_datos = false;
        //$scope.load_datos = false;
    });



    $scope.updateDropDownDepartamentos = function (a) {
        if (a == null) {

            $scope.ciudades = {};
            $scope.comunas = {};
        }
        else {
            var r_c = mantenedor_total.obtener_datos('/faq/rellenar_ciudades/' + a);
            r_c.then(function (successResponse) {

                $scope.ciudades = successResponse.data.ciudades;

                //$scope.departamentos_filter = $filter('filter')($scope.departamentos, { id_region: pais });
                //$scope.load_datos = false;

            },
            function (errorResponse) {
                //$scope.error_datos = false;
                //$scope.load_datos = false;
            });

        }

    }

    $scope.updateDropDownProvincias = function (a) {
        if (a == null) {

            $scope.comunas = {};
        }
        else {
            var r_co = mantenedor_total.obtener_datos('/faq/rellenar_comunas/' + a);
            r_co.then(function (successResponse) {

                $scope.comunas = successResponse.data.comunas;

                // $scope.comunas_filter = $filter('filter')($scope.comuna, { id_ciudad: a });

            },
            function (errorResponse) {
                //$scope.error_datos = false;
                //$scope.load_datos = false;
            });


        }
    }


}]);