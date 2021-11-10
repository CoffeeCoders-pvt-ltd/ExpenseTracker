const APP_HELPER = {};

const __ = document.querySelector.bind(document);
const _a = document.querySelectorAll.bind(document);

APP_HELPER.Loaded = (callback) => {
    document.addEventListener('DOMContentLoaded', callback);
};
APP_HELPER.Loaded(() => {
    // TomSelect.define('change_listener', window.change_listener)
});
APP_HELPER.InitializeSingleTypeahead = (elem) => {
    new TomSelect(elem, {
        // plugins: ['change_listener'],
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
            },
            no_results:function(data,escape) {
                return '<div class="no-results"> 😟 No results found for "'+escape(data.input)+'"</div>';
            },
        }
    });
};

APP_HELPER.InitializeTypeahead = () => {
    const elems = document.querySelectorAll('select:not(.ignore-typeahead');
    for (const elem of elems) {
        APP_HELPER.InitializeSingleTypeahead(elem);
    }
};

APP_HELPER.ReplaceTypeaheadOptions = (elem, options) => {
    elem.tomselect.clear();
    elem.tomselect.clearOptions();
    elem.tomselect.addOptions(options);
    elem.dispatchEvent(new Event('change'));
};