const CCache = {
    Cache: new Map(),
    Set(key, value) {
        this.Cache.set(key, value);
        return this;
    },
    Get(key,defaultValue = null) {
        const data = this.Cache.get(key);
        if(typeof data === "undefined") return defaultValue;
        return data;
    },
    Has(key) {
        return this.Cache.has(key);
    },
    Remove(key) {
        this.Cache.delete(key);
        return this;
    },
    Clear() {
        this.Cache.clear();
        return this;
    }
};

const CreateCache = () => ({
    ...CCache,
    Cache: new Map()
})