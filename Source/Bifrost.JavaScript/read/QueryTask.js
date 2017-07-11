Bifrost.namespace("Bifrost.read", {
    QueryTask: Bifrost.tasks.LoadTask.extend(function (query, paging, taskFactory) {
        var scriptSource = (function () {
            var script = $("script[src*='Bifrost/Application']").get(0);

            if (script.getAttribute.length !== undefined) {
                return script.src;
            }

            return script.getAttribute('src', -1);
        }());

        var uri = Bifrost.Uri.create(scriptSource);

        var port = uri.port || "";
        if (!Bifrost.isUndefined(port) && port !== "" && port !== 80) {
            port = ":" + port;
        }

        this.origin = uri.scheme + "://" + uri.host + port;

        var url = this.origin + "/Bifrost/Query/Execute?_q=" + query._generatedFrom;

        var payload = {
            descriptor: {
                nameOfQuery: query._name,
                generatedFrom: query._generatedFrom,
                parameters: query.getParameterValues()
            },
            paging: {
                size: paging.size,
                number: paging.number
            }
        };

        this.query = query._name;
        this.paging = payload.paging;

        var innerTask = taskFactory.createHttpPost(url, payload);

        this.execute = function () {
            var promise = innerTask.execute();
            return promise;
        };
    })
});