document.addEventListener("DOMContentLoaded", () => {
	sc_options = {
		text: 'Мы используем файлы cookie, чтобы вам было удобнее пользоваться нашим сайтом. <br />Продолжая использование сайта, вы соглашаетесь c использованием нами файлов cookies. <br /><a href="Home/Privacy">Наше соглашение</a>.',
		textButton: "Принять",
	};

	function getCookie(name) {
		let matches = document.cookie.match(
			new RegExp(
				"(?:^|; )" +
				name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, "\\$1") +
				"=([^;]*)",
			),
		);

		return matches ? decodeURIComponent(matches[1]) : undefined;
	}

	function setCookie(name, value, options = {}) {
		options = {
			path: "/",
			expires: new Date(Date.now() + 86400e3 * 64).toUTCString(),
			...options,
		};

		if (options.expires instanceof Date) {
			options.expires = options.expires.toUTCString();
		}

		let updatedCookie =
			encodeURIComponent(name) + "=" + encodeURIComponent(value);

		for (let optionKey in options) {
			updatedCookie += "; " + optionKey;
			let optionValue = options[optionKey];
			if (optionValue !== true) {
				updatedCookie += "=" + optionValue;
			}
		}

		document.cookie = updatedCookie;
	}

	let sc_widget = function (options) {
		if (options) {
			let sc_widget = document.createElement("div");
			let sc_widget_text = document.createElement("div");
			let sc_agree_button = document.createElement("button");
			let sc_agree_text = document.createTextNode(options.textButton);

			sc_widget.style = "box-sizing: border-box; width: 100 %; position: fixed; z - index: 1000; bottom: 0; display: flex; flex - wrap: wrap;" +
				" justify - content: center; align - items: center; background: #fffae2; padding: 15px; box - shadow: 0 - 3px 10px rgba(0, 0, 0, 0.1);" +
				" @media(max - width: 768px) { flex - flow: column; }";

			// Текст
			sc_widget_text.style = "width: calc(100 % - 160px); padding: 0 15px; font: 14px \"Open Sans\", sans - serif; line - height: 1.3; color: #654d3b;" +
				"@media(max - width: 768px) { margin - bottom: 15px; text - align: center; width: 100 %; } a { color: #654d3b; text - decoration: underline; &: visited { color: #654d3b; }" +
				"&: hover { transition: 0.3s; color: #f6b778; }}";
			sc_widget_text.innerHTML = options.text;

			// Кнопка
			sc_agree_button.style = "width: 120px; font: bold 14px \"Open Sans\", sans - serif; border - radius: 3px; height: 40px; border: none; background: #f6b778;" +
				"text - transform: uppercase; color: #654d3b; transition: 0.3s; cursor: pointer; &: hover { background: #654d3b; color: #fff; transition: 0.3s;}";
			sc_agree_button.append(sc_agree_text);

			sc_widget.append(sc_widget_text);
			sc_widget.append(sc_agree_button);

			sc_agree_button.onclick = () => {
				setCookie('sc_cookies', true);
				sc_widget.remove();
			}

			document.body.append(sc_widget);
		}
	};

	if (!getCookie("sc_cookies")) {
		sc_widget(sc_options);
	}

	else {
		console.log(getCookie("sc_cookies"));
	}
});
