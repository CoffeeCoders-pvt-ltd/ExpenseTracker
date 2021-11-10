const APP_HELPER = {};

APP_HELPER.Loaded = (callback) => {
    document.addEventListener('DOMContentLoaded', callback);
};

APP_HELPER.InitializeSingleTypeahead = (elem) => {
    new TomSelect(elem, {
        allowEmptyOption: true,
        placeholder: elem.dataset.placeholder ?? "Select an option",
        render: {
            option: function (data, escape) {
                const icon = data.icon ? `<i class="${data.icon}"></i>  ` : '';
                return '<div>' +
                    '<span class="title">' + icon + escape(data.text) + '</span>' +
                    '</div>';
            },
            item: function (data, escape) {
                const icon = data.icon ? `<i class="${data.icon}"></i>  ` : '';
                 return '<div>' +
                    '<span class="title">' + icon + escape(data.text) + '</span>' +
                    '</div>';
            }
        }
    });
};

APP_HELPER.InitializeTypeahead = () => {
    const elems = document.querySelectorAll('select:not(.ignore-typeahead');
    for (const elem of elems) {
        APP_HELPER.InitializeSingleTypeahead(elem);
    }
};