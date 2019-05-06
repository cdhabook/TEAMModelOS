<template>
    <div style="width:50%">
        <div class="selection-component row">
            <Icon type="md-home" size="20" style="margin-right:10px;" />
            <label class="selection-label">{{$t('formConfigP.school')+':'}}</label>
            <Row style="width:90%;">

                <Col :xs="18" :sm="6" :md="5" :lg="4">
                <Select v-model="country.countryId" class="my-select" :placeholder="$t('formConfigP.country')" @on-change="getSelctValue('country',$event)" :disabled="$i18n.locale.indexOf('en') != -1">
                    <Option v-for="item in countryData" :value="item.countryId" :key="item.sysAddID">{{ item.countryName }}</Option>
                </Select>
                </Col>
                <Col :xs="18" :sm="6" :md="5" :lg="4">
                <Select v-model="schoolInfo.province.provinceId" v-show="country.countryId != 'TW'" class="my-select" :placeholder="$t('formConfigP.province')" :disabled="(checkCountry != 'CN' &&  checkCountry != 'TW' && checkCountry != null) || $i18n.locale.indexOf('en') != -1" clearable ref="province" @on-change="getSelctValue('province',$event)">
                    <Option v-for="item in provinceData" :value="item.provinceId" :key="item.sysAddID">{{ item.provinceName }}</Option>
                </Select>
                </Col>
                <Col :xs="18" :sm="6" :md="5" :lg="4">
                <Select v-model="schoolInfo.city.cityId" class="my-select" :placeholder="$t('formConfigP.city')" :disabled="(checkCountry != 'CN' &&  checkCountry != 'TW' && checkCountry != null) || $i18n.locale.indexOf('en') != -1" clearable ref="city" @on-change="getSelctValue('city',$event)">
                    <Option v-for="item in cityData" :value="item.cityId" :key="item.sysAddID">{{ item.cityName }}</Option>
                </Select>
                </Col>
                <Col :xs="18" :sm="6" :md="5" :lg="12">
                <Select v-model="schoolInfo.school.name" class="my-select" :placeholder="$t('formConfigP.school')" :disabled="(checkCountry != 'CN' &&  checkCountry != 'TW' && checkCountry != null) || $i18n.locale.indexOf('en') != -1" clearable ref="school" @on-change="getSelctValue('school',$event)" filterable :loading="loading" not-found-text="">
                    <Option v-for="item in schoolData" :value="item.name" :key="item.sysAddID">{{ item.name }}</Option>
                </Select>
                </Col>
            </Row>
        </div>
        <div class="selection-component " style="margin-left:26px;">
            <label class="selection-label"></label>
            <Row style="width:90%; margin-top:20px;" v-if="(checkCountry != 'CN' &&  checkCountry != 'TW' && checkCountry != null) || $i18n.locale.indexOf('en') != -1">
                <Col :xs="24" :sm="6" :md="5" :lg="6" style="display:none;">
                <Input v-model="schoolInfoInput.country" class="my-select" placeholder="country" style="width: 95%;" />
                </Col>
                <Col :xs="24" :sm="6" :md="5" :lg="6">

                <Input v-model="schoolInfoInput.city" class="my-select" placeholder="city" style="width: 95%" />
                </Col>
                <Col :xs="24" :sm="6" :md="5" :lg="12">
                <Input v-model="schoolInfoInput.school" class="my-select" placeholder="school" style="width: 90%;" @on-change="getInputValue" />
                </Col>
            </Row>
        </div>


    </div>
</template>

<script>
    export default {
        props: {

        },
        data() {
            return {
                model: "",
                zhCnData: [],
                zhTwData: [],
                enUsData: [],
                schoolData: [],
                country: {},
                schoolInfoInput: {
                    province: '',
                    city: '',
                    school: '',
                },
                schoolInfo: {
                    province: {},
                    city: {},
                    school: {},
                },
                loading: false
            };
        },
        methods: {
            //getValue() {
            //    this.$emit("get-value", [
            //        this.model,
            //        this.selections.field
            //    ]);
            //},
            getCountryInfo(data) {
                let result = [];
                let currentCountry = '';
                for (let i = 0; i < data.length; i++) {
                    if (currentCountry != data[i].countryName && data[i].countryName != null && data[i].provinceName == null && data[i].cityName == null) {
                        currentCountry = data[i].countryName;
                        result.push(data[i]);
                    }
                }
                return result;
            },
            getProvinceInfo(data, country) {
                let result = [];
                let currentProvince = '';
                for (let i = 0; i < data.length; i++) {
                    if (currentProvince != data[i].provinceName && data[i].provinceName != null && data[i].countryId == country.countryId) {
                        currentProvince = data[i].provinceName;
                        result.push(data[i]);
                    }
                }
                return result;
            },
            getCityInfo(data, province) {
                let result = [];
                let currentCity = '';
                if (province != undefined) {
                    for (let i = 0; i < data.length; i++) {
                        if (currentCity != data[i].cityName && data[i].cityName != null && data[i].provinceId == province.provinceId) {
                            currentCity = data[i].cityName;
                            result.push(data[i]);
                        }
                    }
                }
                return result;
            },
            getSchoolInfo(CountryId, ProvinceId, CityId) {
                let params = {
                    CountryId: CountryId,
                    ProvinceId: ProvinceId,
                    CityId: CityId
                }
                this.loading = true;

                //发送请求获取学校列表
                this.$api.getSchoolApi(params).then(
                    (res) => {
                        if (res.error == null) {
                            if (res.result.data.length == 0 || !res.result.data) {
                                alert("暂未收录此地区学校，请手动输入！");
                            } else {
                                this.schoolData = res.result.data;
                                //this.schoolInfo.school = res.result.data[0];
                            }

                        } else {
                            alert("服务器错误！");
                        }
                        this.loading = false;
                    },
                    (err) => {
                        console.log(err);
                        this.loading = false;
                    }
                );
            },
            getSelctValue(flag, value) {
                if (flag == 'country') {
                    this.$refs.school.clearSingleSelect();
                    this.$refs.city.clearSingleSelect();
                    this.$refs.province.clearSingleSelect();
                } else if (flag == 'province') {
                    this.$refs.school.clearSingleSelect();
                    this.$refs.city.clearSingleSelect();
                } else if (flag == 'city') {
                    this.$refs.school.clearSingleSelect();
                    this.getSchoolInfo(this.country.countryId, this.schoolInfo.province.provinceId, this.schoolInfo.city.cityId);
                } else if (flag == 'school') {
                    this.schoolInfo['checkCountry'] = this.checkCountry;
                    this.schoolInfo['country'] = this.country;
                    this.$emit("school-info", this.schoolInfo);
                }
            },
            getInputValue() {
                this.schoolInfoInput['checkCountry'] = this.checkCountry;
                this.schoolInfoInput['country'] = this.country;
                this.$emit("school-info", this.schoolInfoInput);
            }
        },
        computed: {
            countryData() {
                let countryInfo = [];
                if (this.$i18n.locale == 'zh-CN') {
                    countryInfo = this.getCountryInfo(this.zhCnData);

                } else if (this.$i18n.locale == 'zh-TW') {
                    countryInfo = this.getCountryInfo(this.zhTwData);
                } else if (this.$i18n.locale.indexOf('en') != -1) {
                    countryInfo = this.getCountryInfo(this.enUsData);
                }
                return countryInfo;
            },
            provinceData() {
                let provinceInfo = [];
                if (this.$i18n.locale == 'zh-CN') {
                    provinceInfo = this.getProvinceInfo(this.zhCnData, this.country);
                } else if (this.$i18n.locale == 'zh-TW') {
                    provinceInfo = this.getProvinceInfo(this.zhTwData, this.country);
                } else if (this.$i18n.locale.indexOf('en') != -1) {
                    provinceInfo = this.getProvinceInfo(this.enUsData, this.country);
                }
                return provinceInfo;
            },
            cityData() {
                let cityInfo = [];
                if (this.$i18n.locale == 'zh-CN') {
                    cityInfo = this.getCityInfo(this.zhCnData, this.schoolInfo.province);
                } else if (this.$i18n.locale == 'zh-TW') {
                    cityInfo = this.getCityInfo(this.zhTwData, this.schoolInfo.province);
                } else if (this.$i18n.locale.indexOf('en') != -1) {
                    cityInfo = this.getCityInfo(this.enUsData, this.schoolInfo.province);
                }
                return cityInfo;
            },
            checkCountry() {
                //if (this.schoolInfo.country.countryId == undefined) {
                //    return 0;
                //}
                //if (this.schoolInfo.country.countryId == 'CN' || this.schoolInfo.country.countryId == 'TW') {
                //    return 1;
                //} else {
                //    return 2;
                //}
                if (this.country.countryId == undefined) {
                    return null;
                } else {
                    return this.country.countryId;
                }
            }
        },
        mounted() {
            let area = require("../assets/static/area.json");
            for (let i = 0; i < area.length; i++) {
                if (area[i].lang == 'en-us') {
                    this.enUsData.push(area[i]);
                } else if (area[i].lang == 'zh-tw') {
                    this.zhTwData.push(area[i]);
                } else if (area[i].lang == 'zh-cn') {
                    this.zhCnData.push(area[i]);
                }
            }

        }
    };
</script>

<style scoped>
    .selection-component {
        width: 100%;
        position: relative;
        display: flex;
        flex-direction: row;
        align-items: center;
    }

    .ivu-select-selection {
        width: 150px !important;
    }

    .selection-label {
        width: 80px;
        font-size: 15px;
        /*color: #67B6E5;*/
    }

    .my-select {
        width: 90%;
    }

        .my-select > > > .ivu-select-selection {
            border: 1px solid #67B6E5;
        }

        .my-select > > > .ivu-select-selected-value {
            color: #67B6E5;
            font-size: 15px;
        }

        .my-select > > > .ivu-select-input {
            color: #67B6E5;
            font-size: 13px;
        }
</style>
