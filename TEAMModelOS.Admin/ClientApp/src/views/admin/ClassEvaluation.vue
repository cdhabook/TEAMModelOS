<template>
    <div>
        <h2 class="title">班级评价标准</h2>
        <Button type="primary" shape="circle" :icon="importExcel ? 'md-search' : 'md-add-circle'" class="toggle-btn" @click="toggle">{{importExcel?'查看标准':'批量导入'}}</Button>
        <ImportExcel v-if="importExcel" class="animated slideInUp"></ImportExcel>
        <Table v-if="!importExcel" class="animated slideInUp" stripe :columns="columns" :data="data"></Table>
    </div>
</template>
<script>
    import ImportExcel from '@/components/ImportExcel.vue'
    export default {
        data() {
            return {
                importExcel: false,
                columns: [
                    {
                        title: '评价类型',
                        key: 'name'
                    },
                    {
                        title: '检查名称',
                        key: 'age'
                    },
                    {
                        title: '检查类型',
                        key: 'address'
                    },
                    {
                        title: '表扬/待改进',
                        key:'praise'
                    },
                    {
                        title: '指标',
                        key: 'standard'
                    },
                    {
                        title: '分值',
                        key: 'score'
                    }
                ],
                data:[]
            }
        },
        components: {
            ImportExcel
        },
        methods: {
            toggle() {
                this.importExcel = !this.importExcel;
            }
        },
        mounted() {
        }
    }
</script>
<style scoped>
    .title {
        text-align: center;
        margin-bottom: 15px;
    }
    .toggle-btn {
        padding: 3px 12px;
        float: right;
        margin-top: -45px;
    }
</style>
