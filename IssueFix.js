  getTrackedURL: function getTrackedURL() {
    try {
      var spPageContextInfoData = this.get('spPageContextInfo');
      var result = window.location.protocol + '//' + window.location.hostname + (window.location.port ? ':' + window.location.port : '');

      var queryParamsToCheck = ['RecSrc', 'RootFolder']; // order is important
      var pathInQueryParam = '';
      if (typeof GetUrlKeyValue !== 'undefined') {
        for (var i = 0; i < queryParamsToCheck.length; i++) {
          if (GetUrlKeyValue(queryParamsToCheck[i], false).length) {
            pathInQueryParam = GetUrlKeyValue(queryParamsToCheck[i], false);
            break;
          }
        }
      }

      // if RecSrc or RootFolder exist return the correct URL
      if (pathInQueryParam.length) {
        result += pathInQueryParam;

        return decodeURIComponent(Utils.removeBackSlashString(result));
      } else if (typeof AjaxNavigate$convertMDSURLtoRegularURL === 'function') {
        result = decodeURIComponent(AjaxNavigate$convertMDSURLtoRegularURL(document.location.href));

        if (result.indexOf('?') !== -1) {
          var urlWithoutQueryParams = result.substring(0, result.indexOf('?'));
          var queryParamsString = result.substring(result.indexOf('?'));

          // set clear url
          result = urlWithoutQueryParams;
          
          // when we have mote than one query parameter  
          if (queryParamsString.length && queryParamsString.indexOf('&') !== -1) {
            // keep only ID param
            var params = queryParamsString.trimLeft('?').toUpperCase().split('&');
            var idParam = params.filter(param => param.startsWith('ID='));
            queryParamsString = `?${idParam}`;
          }

          result += queryParamsString;
        }

        return Utils.removeBackSlashString(result);
      } else if (spPageContextInfoData.serverRequestPath.match(_spPageContextInfo.webServerRelativeUrl)) {
        result += spPageContextInfoData.serverRequestPath;
      } else {
        result += spPageContextInfoData.webServerRelativeUrl;
        result += spPageContextInfoData.serverRequestPath;
      }

      var queryString = document.location.search;
      var _document = document,
          hash = _document.location.hash;


      if (!queryString.length) {
        if (hash.length && hash.match('\\?')) {
          if (hash.split('?').length > 1) {
            queryString = '?' + hash.split('?')[1];
          }
        }
      }

      if (queryString.length) {
        if (queryString.indexOf('&') !== -1) {
          result += queryString.substring(0, queryString.indexOf('&'));
        } else {
          result += queryString;
        }
      }
    } catch (error) {
      LogHandler.error('Error occurred when trying to build a tracked URL. ', error);
    }

    return decodeURIComponent(Utils.removeBackSlashString(result));
  }
