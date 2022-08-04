/**
 * Функция для вставки отстутпа от "футера" страницы, чтобы контент не попадал за него.
 */
function updateMarginSpace() {
    var main = document.getElementById("divider");
    var footer = document.getElementById("footer-block");

    // Чтобы футер не подходил очень близко к контенту, увеличиваем отступ.
    main.style.marginTop = footer.clientHeight + 25 + 'px';
}

/**
 * Обновляет данные о текущей паре и времени её завершения.
 */
function insertLessonsInfo() {
    var firstElement = document.getElementById("current-lesson-description");
    var secondElement = document.getElementById("current-lesson-end-estimated-time");

    updateInfoAboutLessons(firstElement, secondElement);
}

/**
 * Функция, дополняющая элемент с дополнительными ссылками в зависимости от ширины экрана.
 */
function updateLinksAttributes() {
    var element = document.getElementById("links");

    if (window.innerWidth > 680) {
        element.removeAttribute("data-bs-toggle");
        element.removeAttribute("aria-expanded");
    }

    else {
        element.setAttribute("data-bs-toggle", "dropdown");
        element.setAttribute("aria-expanded", "false");
    }
}

/**
 * Событие, происходящее при загрузке страницы.
 * Вручную вызывает функцию вставки даты и времени, затем устанавливает её на интервал.
 *
 * Также устанавливает отступ от нижней границы ("футера").
 */
function onLoad() {
    updateMarginSpace();
    insertLessonsInfo();
    updateLinksAttributes();

    setInterval(insertLessonsInfo, 60000);
}
