<template>
    <div class="about">
        <select-school style="width:55%;float:left;" @school-info="getSchoolInfo"></select-school>
        <Tag type="dot" closable v-show="schoolName != '00'">{{schoolName}}</Tag>
        <!--<Form :model="formItem" :label-width="80">
            <FormItem label="Input">
                <Input  placeholder="Enter something..."></Input>
            </FormItem>
        </Form>-->
    </div>
</template>
<script>
    import SelectSchool from "@/components/SelectSchool.vue"
    export default {
        data() {
            return {
                schoolInfo: {}
            }
        },
        components: {
            SelectSchool
        },
        mounted() {

        },
        methods: {
            getSchoolInfo(data) {
                console.log(data);
                this.schoolInfo = data;
            }
        },
        computed: {
            schoolName() {
                if (this.schoolInfo.checkCountry == 'CN' || this.schoolInfo.checkCountry == 'TW') {
                    return this.schoolInfo.school.name;
                } else if (this.schoolInfo.checkCountry == null) {
                    return '00';
                } else {
                    return this.schoolInfo.school;
                }
            }
        }
    }
</script>
<style>
</style>