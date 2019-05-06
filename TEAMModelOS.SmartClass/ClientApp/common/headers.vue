<template>
    <Row class="header_box">
      <Col span="8" class="logos">
           <div class="header_title">智慧大云<span>TEAM Model OS</span></div>
           <div class="header_title_instrument"><Icon type="md-pin" /><span>{{itemstitle}}</span></div>
      </Col>
      <Col span="16" class="header_right_box">
        <div class="skip">
          <li class="skip_box"><img src="../icons/student-analysis/header_icon1.png"><span class="skip_title">智慧城市管理</span></li>
          <li class="skip_box"><img src="../icons/student-analysis/header_icon2.png"><span class="skip_title">教材资源管理</span></li>
          <li class="skip_box"><img src="../icons/student-analysis/header_icon3.png"><span class="skip_title">学习活动服务</span></li>
          <li class="skip_box"><img src="../icons/student-analysis/header_icon4.png"><span class="skip_title">班级智慧服务</span></li>
        </div>
        <div class="portrait">
          <div class="portrait_img"><img src="../icons/student-analysis/touxiang.png"  @click="value3 = true"></div>
          <Drawer title="个人信息"
                  v-model="value3"
                  width="720"
                  :mask-closable="false"
                  :styles="styles">
            <Form :model="formData">
              <!--<Row :gutter="32">-->
                <!--姓名-->
                <!--<Col span="12">
                <FormItem label="Name" label-position="top">
                  <Input v-model="formData.name" placeholder="please enter user name" />
                </FormItem>
                </Col>-->
                <!--URL-->
                <!--<Col span="12">
                <FormItem label="Url" label-position="top">
                  <Input v-model="formData.url" placeholder="please enter url">
                  <span slot="prepend">http://</span>
                  <span slot="append">.com</span>
                  </Input>
                </FormItem>
                </Col>-->
              <!--</Row>-->
              <Row :gutter="32">
                <Col span="19">
                <FormItem label="身份切换:" label-position="top">
                  <Select v-model=" fordatamodelSelect" placeholder="选择所在学校身份内容">
                    <Option v-for="(item,index) in fordatamodel" :value="item.name">
                      {{item.name}}{{item.rolename}}
                    </Option>
                  </Select>
                </FormItem>
                </Col>
              </Row>
              <!--<Row :gutter="32">
                <Col span="12">
                <FormItem label="Approver" label-position="top">
                  <Select v-model="formData.approver" placeholder="please choose the approver">
                    <Option value="jobs">Steven Paul Jobs</Option>
                    <Option value="ive">Sir Jonathan Paul Ive</Option>
                  </Select>
                </FormItem>
                </Col>
                <Col span="12">
                <FormItem label="DateTime" label-position="top">
                  <DatePicker v-model="formData.date" type="daterange" placeholder="please select the date" style="display: block" placement="bottom-end"></DatePicker>
                </FormItem>
                </Col>
              </Row>
              <FormItem label="Description" label-position="top">
                <Input type="textarea" v-model="formData.desc" :rows="4" placeholder="please enter the description" />
              </FormItem>-->
            </Form>
            <div class="demo-drawer-footer">
              <Button style="margin-right: 8px" @click="value3 = false">取消</Button>
              <Button type="primary" @click="value3 = false">确认</Button>
            </div>
          </Drawer>
        </div>
      </Col>
    </Row>
</template>

<script>
    export default {
    name: "headers",
    props: ['parentToChild', 'identityselect'],
    data() {
      return {
        value3: false,
        itemstitle: '',
        fordatamodel: '',
        fordatamodelSelect:'',
        styles: {
          height: 'calc(100% - 55px)',
          overflow: 'auto',
          paddingBottom: '53px',
          position: 'static'
        },
        formData: {
          name: '',
          url: '',
          owner: '',
          type: '',
          approver: '',
          date: '',
          desc: ''
        },

      }
    },
    created() {
      this.itemstitle = this.parentToChild;
      this.fordatamodel = this.identityselect;
      this.fordatamodelSelect = this.identityselect[0].name;
    },
    methods: {
      popup() {
        
      },
      handleOpen() {
        this.visible = true;
      },
      handleClose() {
        this.visible = false;
      }
    },
    }
</script>
<style>
  .ivu-drawer-right {
    width:400px !important;
  }
</style>
<style scoped>
.header_box{
  width:100%;
  height:60px;
  background: url("../icons/student-analysis/header.png") no-repeat;
  background-size: cover;
}
.logos{
  padding-left: 2%;
}
  .header_title{
    width:43%;
    height:60px;
    float: left;
    color:#fff;
    font-size: 1.5rem;
    line-height: 3.5rem;
    font-weight: bold;
  }
  .header_title span{
    padding-left: 2%;
    font-size: 0.7rem;
    height:60px;
  }
  .header_title_instrument{
    width:50%;
    float: left;
    color:#fff;
    padding-left: 5%;
    font-size: 1.5rem;
    line-height: 3.5rem;
    height:60px;
  }
  .header_title_instrument span{
    font-size: 1.0rem;
  }
  .header_right_box{
    padding-left: 10%;
    height:60px;
  }
  .skip{
    width:85%;
    padding-top: 0.2%;
    display:inline
  }
  .skip_box{
    width:18.5%;
    list-style-type:none;
    float: left;
    margin-left: 2%;
    margin-top: 0.5%;
    position:relative;
    padding-left:25px;
  }
  .skip_box>img{
    width:20px;
    position:absolute;
    left:0;top:50%;
    transform:translateY(-50%)
  }
  .skip_title{
    font-size: 1.0rem;
    color:#fff;
    line-height: 3.5rem;
  }
  .portrait{
    width:13%;
    float: right;
    display: block;
    margin-top: 0.5%;
  }
  .portrait_img{
    width:100%
  }
  .portrait_img img{
    width:28%;
    margin-top: 3%;
  }
  .img_box{
    width:20px;
    height:20px;
    padding-top:8%;
  }
    .img_box img {
     width:80%;
     margin-top:25%;
    }
  .two_icon {
    margin-top: 25% !important;
  }



  .demo-drawer-footer {
    width: 100%;
    position: absolute;
    bottom: 0;
    left: 0;
    border-top: 1px solid #e8e8e8;
    padding: 10px 16px;
    text-align: right;
    background: #fff;
  }
</style>
