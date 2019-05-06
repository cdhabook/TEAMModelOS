<template>
    <div class="form-body">
        <Row>
            <div class="ivu-upload-list-file" v-if="file !== null">
                <Icon type="ios-stats"></Icon>
                {{ file.name }}
                <Icon v-show="showRemoveFile" type="ios-close" class="ivu-upload-list-remove" @click.native="handleRemove()"></Icon>
            </div>
        </Row>
        <Row>
            <transition name="fade">
                <Progress v-if="showProgress" :percent="progressPercent" :stroke-width="2">
                    <div v-if="progressPercent == 100">
                        <Icon type="ios-checkmark-circle"></Icon>
                        <span>成功</span>
                    </div>
                </Progress>
            </transition>
        </Row>
        <Row>
            <Table :columns="tableTitle" :data="tableData" :loading="tableLoading" style="max-height:590px; overflow-y:scroll;"></Table>
            <div style="background-color:white; width:100%; height:50px;">
                <Upload action="" :before-upload="handleBeforeUpload" accept=".xls, .xlsx" style="display:block; float:right;">
                    <Button :loading="uploadLoading" @click="openUploadFile" icon="ios-folder-open" class="btn">
                        打开文件
                    </Button>
                    <Button :loading="uploadLoading" @click.stop="sendUploadFile" icon="md-cloud-upload" class="btn">
                        上传文件
                    </Button>
                </Upload>
            </div>
        </Row>
    </div>
</template>
<script>
    import excel from '../tools/excel.js'
    export default {
        data() {
            return {
                uploadLoading: false,
                progressPercent: 0,
                showProgress: false,
                showRemoveFile: false,
                file: null,
                tableData: [],
                tableTitle: [],
                tableLoading: false
            }
        },
        methods: {
            initUpload() {
                this.file = null
                this.showProgress = false
                this.loadingProgress = 0
                this.tableData = []
                this.tableTitle = []
            },
            openUploadFile() {
                this.initUpload()
            },
            handleRemove() {
                this.initUpload()
                this.$Message.info('打开的文件已删除！')
            },
            handleBeforeUpload(file) {
                const fileExt = file.name.split('.').pop().toLocaleLowerCase()
                if (fileExt === 'xlsx' || fileExt === 'xls') {
                    this.readFile(file)
                    this.file = file
                } else {
                    this.$Notice.warning({
                        title: '文件类型错误',
                        desc: '文件：' + file.name + '不是EXCEL文件，请选择后缀为.xlsx或者.xls的EXCEL文件。'
                    })
                }
                return false
            },
            // 读取文件
            readFile(file) {
                const reader = new FileReader();
                reader.readAsArrayBuffer(file);
                reader.onloadstart = e => {
                    this.uploadLoading = true
                    this.tableLoading = true
                    this.showProgress = true
                }
                reader.onprogress = e => {
                    this.progressPercent = Math.round(e.loaded / e.total * 100)
                }
                reader.onerror = e => {
                    this.$Message.error('文件读取出错')
                }
                reader.onload = e => {
                    this.$Message.info('文件读取成功');
                    const data = e.target.result;
                    const { header, results } = excel.read(data, 'array');
                    const tableTitle = header.map(item => { return { title: item, key: item } });
                    this.tableData = results;
                    this.tableTitle = tableTitle;
                    this.uploadLoading = false;
                    this.tableLoading = false;
                    this.showRemoveFile = true;
                    console.log(this.tableData);
                }
            },
            sendUploadFile() {
            }
        },
        mounted() {
        }
    }
</script>
<style scoped>
    .btn {
        background: white;
        margin-top: 15px;
        font-size: 15px;
        padding: 5px 10px;
        width: 120px;
        padding: 4px 10px;
        margin-left: 10px;
    }
</style>
