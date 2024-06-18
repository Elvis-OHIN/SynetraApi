(function () {
    const overrider = () => {
        const swagger = window.ui;
        if (!swagger) {
            return;
        }

        ensureAuthorization(swagger);
        reloadSchemaOnAuth(swagger);
        clearInputPlaceHolder(swagger);
        showLoginUI(swagger);
    }

    const getAuthorization = (swagger) => swagger.auth()._root.entries.find(e => e[0] === 'authorized');
    const isAuthorized = (swagger) => {
        const auth = getAuthorization(swagger);
        return auth && auth[1].size !== 0;
    };

    const ensureAuthorization = (swagger) => {
        const getBearer = () => {
            const auth = getAuthorization(swagger);
            const def = auth[1]._root.entries.find(e => e[0] === 'Bearer');
            if (!def)
                return undefined;

            const token = def[1]._root.entries.find(e => e[0] === 'value');
            if (!token)
                return undefined;

            return token[1];
        }

        const fetch = swagger.fn.fetch;
        swagger.fn.fetch = (req) => {
            if (!req.headers.Authorization && isAuthorized(swagger)) {
                const bearer = getBearer();
                if (bearer) {
                    req.headers.Authorization = bearer;
                }
            }
            return fetch(req);
        }
    };

    const reloadSchemaOnAuth = (swagger) => {
        const getCurrentUrl = () => {
            const spec = swagger.getState()._root.entries.find(e => e[0] === 'spec');
            if (!spec)
                return undefined;

            const url = spec[1]._root.entries.find(e => e[0] === 'url');
            if (!url)
                return undefined;

            return url[1];
        }
        const reload = () => {
            const url = getCurrentUrl();
            if (url) {
                swagger.specActions.download(url);
            }
        };

        const handler = (caller, args) => {
            const result = caller(args);
            if (result.then) {
                result.then(() => reload())
            }
            else {
                reload();
            }
            return result;
        }

        const auth = swagger.authActions.authorize;
        swagger.authActions.authorize = (args) => handler(auth, args);
        const logout = swagger.authActions.logout;
        swagger.authActions.logout = (args) => handler(logout, args);
    };

    const clearInputPlaceHolder = (swagger) => {
        new MutationObserver(function (mutations, self) {
            var elements = document.querySelectorAll("input[type=text]");
            for (var i = 0; i < elements.length; i++)
                elements[i].placeholder = "";
        }).observe(document, { childList: true, subtree: true });
    }

    const showLoginUI = (swagger) => {
        new MutationObserver(function (mutations, self) {
            var rootDiv = document.querySelector("#swagger-ui > section > div.swagger-ui > div:nth-child(2)");
            if (rootDiv == null)
                return;

            var informationContainerDiv = rootDiv.querySelector("div.information-container.wrapper");
            if (informationContainerDiv == null)
                return;

            var descriptionDiv = informationContainerDiv.querySelector("section > div > div > div.description");
            if (descriptionDiv == null)
                return;

            var info = informationContainerDiv.querySelector("section > div > div");

             
            var loginDiv = info.querySelector("div.login");
            if (loginDiv != null)
                return;

            if (isAuthorized(swagger))
                return;

            for (var i = 0; i < rootDiv.children.length; i++) {
                var child = rootDiv.children[i];
                if (child !== informationContainerDiv)
                    child.remove();
            }

            createLoginUI(info);

        }).observe(document, { childList: true, subtree: true });

        const createLoginUI = function (rootDiv) {
            var div = document.createElement("div");
            div.className = "login";

            rootDiv.appendChild(div);

            var emailLabel = document.createElement("label");
            div.appendChild(emailLabel);

            var emailSpan = document.createElement("span");
            emailSpan.innerText = "Email";
            emailLabel.appendChild(emailSpan);

            var emailInput = document.createElement("input");
            emailInput.type = "text";
            emailInput.style = "margin-left: 10px; margin-right: 10px;";
            emailLabel.appendChild(emailInput);

            var passwordLabel = document.createElement("label");
            div.appendChild(passwordLabel);

            var passwordSpan = document.createElement("span");
            passwordSpan.innerText = "Mot de passe";
            passwordLabel.appendChild(passwordSpan);

            var passwordInput = document.createElement("input");
            passwordInput.type = "password";
            passwordInput.style = "margin-left: 10px; margin-right: 10px;";
            passwordLabel.appendChild(passwordInput);

            var loginButton = document.createElement("button")
            loginButton.type = "submit";
            loginButton.type = "button";
            loginButton.classList.add("btn");
            loginButton.classList.add("auth");
            loginButton.classList.add("authorize");
            loginButton.classList.add("button");
            loginButton.innerText = "Login";
            loginButton.onclick = function () {
                var email = emailInput.value;
                var password = passwordInput.value;

                if (email === "" || password === "") {
                    alert("Insérez l'e-mail et le mot de passe!");
                    return;
                }

                login(email, password);
            };

            div.appendChild(loginButton);
        }

        const login = function (email, password) {
            var xhr = new XMLHttpRequest();

            xhr.onreadystatechange = function () {
                if (xhr.readyState == XMLHttpRequest.DONE) {
                    if (xhr.status == 200) {
                        var response = JSON.parse(xhr.responseText);

                        var accessToken = response.accessToken;

                        var obj = {
                            "Bearer": {
                                "name": "Bearer",
                                "schema": {
                                    "type": "apiKey",
                                    "description": "Please enter token",
                                    "name": "Authorization",
                                    "in": "header"
                                },
                                value: "Bearer " + accessToken
                            }
                        };

                        swagger.authActions.authorize(obj);
                    } else if (xhr.status == 401) {
                        alert("Non autorisé : e-mail ou mot de passe invalide. Veuillez réessayer.");
                    } 
                }
            };

            xhr.open("POST", "/api_elvis/login", true);
            xhr.setRequestHeader("Content-Type", "application/json");

            var json = JSON.stringify({ "email": email, "password": password });
            xhr.send(json);
        }
    }

    window.addEventListener('load', () => setTimeout(overrider, 0), false);
    window.addEventListener("load", function () {
        setTimeout(function () {
            
            var logo = document.getElementsByClassName('link');
            logo[0].children[0].alt = "Synetra";
            logo[0].children[0].src = "/api_elvis/swagger-ui/asset/logo-with-name.png";

            var link = document.querySelector("link[rel*='icon']") || document.createElement('link');;
            document.head.removeChild(link);
            link = document.querySelector("link[rel*='icon']") || document.createElement('link');
            document.head.removeChild(link);
 
            var linkIcon32 = document.createElement('link');
            linkIcon32.type = 'image/png';
            linkIcon32.rel = 'icon';
            linkIcon32.href = '/api_elvis/swagger-ui/asset/logo-synetra.png';
            linkIcon32.sizes = '32x32';
            document.getElementsByTagName('head')[0].appendChild(linkIcon32);

            var linkIcon16 = document.createElement('link');
            linkIcon16.type = 'image/png';
            linkIcon16.rel = 'icon';
            linkIcon16.href = '/api_elvis/swagger-ui/asset/logo-synetra.png';
            linkIcon16.sizes = '16x16';
            document.getElementsByTagName('head')[0].appendChild(linkIcon16);  
        });
    });

   
})();
