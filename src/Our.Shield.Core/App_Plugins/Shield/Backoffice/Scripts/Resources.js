﻿angular
  .module('umbraco.resources')
  .factory('shieldResource',
    [
      '$http',
      '$q',
      function ($http, $q) {

        var apiRoot = 'backoffice/Shield/ShieldApi/';

        var get = function (url, data) {
          var deferred = $q.defer();

          data = data || {};

          $http
            .get(apiRoot + url,
              {
                params: data
              })
            .then(function (response) {
              return deferred.resolve(response.data);
            },
              function (response) {
                return deferred.resolve(response);
              });

          return deferred.promise;
        };

        var post = function (url, data) {
          var deferred = $q.defer();

          $http({
            method: 'POST',
            url: apiRoot + url,
            data: JSON.stringify(data),
            dataType: 'json',
            headers: {
              'Content-Type': 'application/json'
            }
          }).then(function (response) {
            return deferred.resolve(response.data);
          },
            function (response) {
              return deferred.resolve(response);
            });

          return deferred.promise;
        };

        return {
          deleteEnvironment: function (id) {
            return post('DeleteEnvironment', { id: id });
          },
          getEnvironments: function () {
            return get('GetEnvironments');
          },
          getJournals: function (id, page, orderBy, orderByDirection) {
            return get('Journals',
              {
                id: id,
                page: page,
                orderBy: orderBy,
                orderByDirection: orderByDirection
              });
          },
          getView: function (id) {
            return get('View',
              {
                id: id
              });
          },
          postConfiguration: function (id, config) {
            config.id = id;
            return post('WriteConfiguration', config);
          },
          postEnvironment: function (environment) {
            return post('WriteEnvironment', environment);
          },
          setEnvironmentsSortOrder: function (environments) {
            return post('SortEnvironments', environments);
          }
        };
      }
    ]
  );