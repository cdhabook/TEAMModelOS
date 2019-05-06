import {get, post} from "./http.js"

const getSchoolUrl = 'api/common/getSchool';
const testUrl = 'api/test/test';
function handleRequestData(url, data) {
    return {
        method: url.substring(url.lastIndexOf("/") + 1, url.length),
        params: data
    }
}
const api = {
    getSchoolApi(data) {
        return post(getSchoolUrl, handleRequestData(getSchoolUrl, data));
    },
    testApi(params) {
        return get(testUrl, params);
    }
}

export default api;