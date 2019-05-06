<template>
    <div class="animated slideInUp home">
        <h2 class="title">系统角色管理</h2>
        <Table border :columns="tableColumns" :data="tableData" width="80%">
            <template slot-scope="{ row ,index }" slot="role">
                <strong><Input v-model="row.role" placeholder="" :disabled="index != currentIndex" :class="index != currentIndex ? 'disabeld-status' : ''" @on-blur="tableData[index].role = row.role "/></strong>
            </template>
            <template slot-scope="{ row ,index }" slot="auth">
                <Input v-model="row.auth.join(' / ')" placeholder="" :disabled="index != currentIndex" :class="index != currentIndex ? 'disabeld-status' : ''" @on-focus="showDialog" />
            </template>
            <template slot-scope="{ row, index }" slot="operation">
                <Button type="primary" size="small" style="margin-right: 5px" @click="update(index)">{{index != currentIndex ? '修改' : '保存'}}</Button>
                <Button type="error" size="small" @click="remove(index)">删除</Button>
            </template>
        </Table>
        <Button type="info" size="small" @click="addRole()" style="margin-top:15px;">添加用户角色</Button>
        <Modal v-model="dialogStatus"
               @on-ok="confirm"
               @on-cancel="cancel" class="auth-wrap" width="1000px">
            <div slot="header">
                <span class="modal-title">设置角色权限</span>
                <Checkbox style="margin-left:30px;" :value="checkAll" :indeterminate="indeterminate" @click.prevent.native="handleCheckAll">全选</Checkbox>
                <!--<Checkbox style="margin-left:30px;" :indeterminate="indeterminate" @click.prevent.native="handleCheckReverse">反选</Checkbox>-->
            </div>
            <CheckboxGroup v-model="roleAuths" @on-change="checkAllGroupChange">
                <Row>
                    <Col :xs="24" :sm="12" :md="12" :lg="12" v-for="(item,index) in auths" :key="index">
                    <Checkbox class="auth-item" :label="item.name">
                        <div class="auth-content">
                            <p><strong>{{item.name}}</strong></p>
                            <p style="color:#999999">{{item.description}}</p>
                        </div>
                    </Checkbox>
                    </Col>
                </Row>

            </CheckboxGroup>
        </Modal>
    </div>
</template>
<script>
    export default {
        name: 'home',
        components: {

        },
        data() {
            return {
                roleAuths: [],
                indeterminate: false,
                checkAll: false,
                currentIndex: -1,
                dialogStatus:false,
                tableColumns: [
                    {
                        title: '系统角色',
                        slot: 'role',
                        width:'200'
                    },
                    {
                        title: '角色权限',
                        slot: 'auth'
                    },
                    {
                        title: '操作',
                        slot: 'operation',
                        width: 150,
                        align: 'center'
                    }
                ],
                tableData: [
                    {
                        role: "系统管理员",
                        auth: [
                            "系统管理员权限"
                        ]
                    },
                    {
                        role: "系统管理员",
                        auth: [
                            "主管领导权限"
                        ]
                    },
                    {
                        role: "系统管理员",
                        auth: [
                            "校长权限"
                        ]
                    },
                    {
                        role: "系统管理员",
                        auth: [
                            "区域管理权限"
                        ]
                    }
                ],
                auths: [
                    {
                        id: "1",
                        name: "系统管理员权限",
                        description: "拥有系统所有数据的操作权限 "
                    },
                    {
                        id: "2",
                        name: "区域管理权限",
                        description: "拥有系统所有数据的操作权限 "
                    },
                    {
                        id: "3",
                        name: "校长权限",
                        description: "拥有系统所有数据的操作权限 "
                    },
                    {
                        id: "4",
                        name: "主管领导权限",
                        description: "拥有系统所有数据的操作权限 "
                    },
                    {
                        id: "5",
                        name: "班主任理权限",
                        description: "拥有系统所有数据的操作权限 "
                    },
                    {
                        id: "6",
                        name: "科任老师权限",
                        description: "拥有系统所有数据的操作权限 "
                    },
                    {
                        id: "7",
                        name: "学生权限",
                        description: "拥有系统所有数据的操作权限 "
                    }
                ]
            }
        },
        methods: {
            update(index) {
                if (this.currentIndex == -1) {
                    this.currentIndex = index;
                    this.roleAuths = this.tableData[index].auth;
                } else {
                    this.currentIndex = -1;
                }
            },
            remove(index) {
                this.tableData.splice(index, 1);
            },
            confirm() {
                this.tableData[this.currentIndex].auth = this.roleAuths;
            },
            cancel() {
                this.$Message.info('Clicked cancel');
            },
            showDialog() {
                undefined
                this.dialogStatus = true;
            },
            handleCheckAll() {
                if (this.indeterminate) {
                    this.checkAll = false;
                } else {
                    this.checkAll = !this.checkAll;
                }
                this.indeterminate = false;

                if (this.checkAll) {
                    this.roleAuths = [];
                    for (let i = 0; i < this.auths.length; i++) {
                        this.roleAuths.push(this.auths[i].name);
                    }
                    //this.roleAuths = ['香蕉', '苹果', '西瓜'];
                } else {
                    this.roleAuths = [];
                }
            },
            addRole() {
                this.tableData.push({
                    role: "",
                    auth:[]
                });
                this.currentIndex = this.tableData.length - 1;
            },
            checkAllGroupChange(data) {
                if (data.length === this.auths.length) {
                    this.indeterminate = false;//全部选中 √
                    this.checkAll = true;
                } else if (data.length > 0) {
                    this.indeterminate = true;//选中一部分 -
                    this.checkAll = false;
                } else {
                    this.indeterminate = false;//未选中 没有样式
                    this.checkAll = false;
                }
            }
        }
    }
</script>
<style scoped>
    .title {
        text-align:center;
        margin-bottom:15px;
    }
    .disabeld-status /deep/ .ivu-input {
        border: none;
    }

    .disabeld-status /deep/ .ivu-input-disabled {
        background-color: transparent;
        color: black;
    }
    .auth-item {
        width:90%;
        padding:5px 12px 15px 12px;
        margin-left:5%;
        margin-top:10px;
        border-bottom:1px solid #eeeeee;
    }
        .auth-item:hover {
            box-shadow: 0px 1px 2px;
        }
        .auth-item /deep/ .ivu-checkbox {
            float:right;
            margin-top:12px;
        }

    .auth-content {
        text-align: left;
        display:inline;
        float:left;
    }
        .auth-content p {
            font-size:15px;
        }
    .auth-wrap /deep/ .ivu-modal-body {
        max-height:600px;
        overflow-y:scroll;
    }
    .modal-title {
        font-size:16px;
        font-weight:600;
    }

</style>