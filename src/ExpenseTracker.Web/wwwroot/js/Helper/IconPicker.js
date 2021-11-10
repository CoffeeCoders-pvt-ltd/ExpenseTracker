const IconPicker = {
    async GetIcons() {
        return new Promise(async (resolve, reject) => {
            if(localStorage.getItem("icons") != null) {
                resolve(JSON.parse(localStorage.icons));
            }
            else {
                const response = await fetch("/api/icon").then(response => response.json());
                localStorage.setItem("icons", JSON.stringify(response.data));
                resolve(response.data);
            }
        });
    }
};