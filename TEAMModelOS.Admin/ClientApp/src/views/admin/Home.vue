<template>
    <div class="layout">
        <Layout>
            <Header class="header">
                <Menu mode="horizontal" theme="dark" active-name="1">
                    <div class="layout-logo">
                        <img src="@/assets/img/logo.png" height="40" />
                    </div>
                    <div style="float:right;line-height:62px;margin-right:30px;">
                        <Dropdown @on-click="personMenu">
                            <a href="javascript:void(0)">
                                <Avatar src="https://i.loli.net/2017/08/21/599a521472424.jpg" />
                            </a>
                            <DropdownMenu slot="list" >
                                <DropdownItem v-for="(item,index) in menuInfo.personMenu" :key="index" :name ="item.name">{{ item.label }}</DropdownItem>
                            </DropdownMenu>
                        </Dropdown>
                    </div>
                    <div class="layout-nav">
                        <Menu mode="horizontal" theme="dark">
                            <Submenu v-for="(item,index) in menuInfo.systemMenu" :name="item.name" :key="index">
                                <template slot="title">
                                    <Icon :type="item.icon" />
                                    {{item.label}}
                                </template>
                                <MenuItem v-for="(subItem,subIndex) in item.subItem" :key="subIndex" :name="subItem.name" @click.native="getSystemRouter(item.label,subItem.label)">{{subItem.label}}</MenuItem>
                            </Submenu>
                        </Menu>
                    </div>
                </Menu>
            </Header>
            <Layout class="body">
                <Sider hide-trigger style="background: #fff;height:100%;">
                    <Menu active-name="1-2" theme="dark" width="240px" :open-names="['1']" accordion>
                        <Submenu v-for="(item,index) in menuInfo.moralSecondMenu" :key="index" :name="item.name">
                            <template slot="title">
                                <Icon :type="item.icon" size="18"></Icon>
                                {{item.label}}
                            </template>
                            <MenuItem v-for="(subItem, subIndex) in item.subItem" :key="subIndex" :name="subItem.name" :to="subItem.router" @click.native="currentRouter(item.label,subItem.label)">{{subItem.label}}</MenuItem>
                        </Submenu>
                    </Menu>
                </Sider>
                <Layout :style="{padding: '0 80px 24px'}">
                    <Breadcrumb :style="{margin: '24px 0'}" separator="<b class='breadcrumb-separator'>></b>">
                        <BreadcrumbItem v-for="(item,index) in systemRouter" :key="index">{{item}}</BreadcrumbItem>
                        <BreadcrumbItem v-for="(item,index) in breadCrumbList" :key="index">{{item}}</BreadcrumbItem>
                    </Breadcrumb>
                    <Content :style="{padding: '24px', minHeight: '280px', background: '#fff'}">
                        <router-view>
                        </router-view>
                    </Content>
                </Layout>
            </Layout>
        </Layout>
    </div>
</template>

<script>
// @ is an alias to /src

export default {
        data() {
            return {
                menuInfo: {},
                systemRouter: ["平台模块", "德育系统"],
                breadCrumbList: []
            }
        },
        mounted() {
            this.menuInfo = require("@/assets/static/menu-info.json");
            console.log(this.menuInfo);
            this.$api.testApi({
                test: "456"
            }).then(
                (res) => {
                    console.log(res);
                },
                (err) => {
                    console.log(err);
                }
            );
        },
        methods: {
            currentRouter(item, subItem) {
                this.breadCrumbList = this.breadCrumbList.slice(0, 0);
                this.breadCrumbList.push(item);
                this.breadCrumbList.push(subItem);
            },
            getSystemRouter(item, subItem) {
                this.systemRouter = this.systemRouter.slice(0, 0);
                this.breadCrumbList = this.breadCrumbList.slice(0, 0);
                this.systemRouter.push(item);
                this.systemRouter.push(subItem);
            },
            personMenu(name) {
                switch (name) {
                    case 'logout':
                        this.$router.push({
                            path: this.menuInfo.personMenu[2].router
                        });
                        break;
                }
            }
        }
}
</script>
<style scoped>
    .layout {
        background: #f5f7f9;
        position: relative;
        overflow: hidden;
        height:100%;
    }

    .layout-logo {
        border-radius: 3px;
        float: left;
        position: relative;
        top: 10px;
        left: 18px;
        display:flex;
        flex-direction:row;
        align-items:center;
    }

    .layout-nav {
        margin: 0 auto;
        float:right;
        margin-right: 20px;
    }
    .breadcrumb-separator {
        color:#3399ff;
    }
    .layout /deep/ .ivu-layout {
        height: 100%;
    }
    .layout /deep/ .ivu-menu-light {
        height:100%;
        padding-top:15px;
    }
    .layout /deep/ .ivu-layout-header {
        background-color:#001529;
    }
    .header /deep/ .ivu-menu-dark {
        background:#001529;
    }
    .body /deep/ .ivu-menu {
        height:100%;
        padding-top:16px;
    }

</style>