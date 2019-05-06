<template>
    <div class="basics_box">
      <div class="basics">
        <li class="basicslist" v-for="(item,index) in basicsdata"  v-if="index ==0">
          <p class="basicslist_title">{{item.title}}</p>
          <p class="basicslist_num">{{item.num}}<Icon type="ios-arrow-round-up"  style="color:#1acb9f;font-weight: bold;
    font-size: 40px;margin-top:10px;" v-if="icon_show"/></p>
          <p class="basicslist_total">{{item.total}}</p>
          <p class="basicslist_area">{{item.areadata}}</p>
        </li>
        <li v-else class="basicslist">
          <p class="basicslist_title">{{item.title}}</p>
          <p class="basicslist_num">{{item.num}}</p>
          <p class="basicslist_total">{{item.total}}</p>
          <p class="basicslist_area" >{{item.areadata}}</p>
        </li>
      </div>
    </div>
</template>
<script>
    export default {
        name: "basics",
        data(){
          return{
           // basicsdata:[],
            now_url: this.$route.path,
            icon_show: false
          }
    },
    created() {
      this.init();
    },
    computed: {
      basicsdata() {
        return this.$store.state.basicsdata
                  }
                },
    methods:{
      init() {
        if (this.now_url == '/saindex') {
          this.$api.FindBasics({})
            .then((response) => {
              //console.log(response.result.data, 11111);
              this.icon_show = true;
              this.$store.state.basicsdata = response.result.data;
              //console.log(this.xueduan,333333)
            })
        } else if (this.now_url == '/teach') {
          this.$api.FindTeachBasics({})
            .then((response) => {
              this.$store.state.basicsdata = response.result.data;
            })
            }
         },
      }
    }
</script>
