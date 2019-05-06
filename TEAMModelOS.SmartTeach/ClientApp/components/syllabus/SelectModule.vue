<template>
  <div class="center">
    <div class="main-btn centerCol" v-for="item in moduleList" @click="handleRouter(item.link)">
      <Icon type="ios-podium" size="80" />
      <h4 class="color1">{{item.name}}</h4>
    </div>
  </div>
</template>

<script>
  export default {
    name: "selectModule",
    props:["moduleList"],
    methods: {
      handleRouter(path) {
        this.$router.replace(path)
      },
    }
  }
</script>

<style scoped>
  .main-btn {
    height: 210px;
    width: 210px;
    cursor: pointer;
    font-size: 16px;
    font-weight: 200;
    background-color: rgba(179,179,179,.15);
    border: 0;
    margin: 0 10px;
    color: #ccc;
  }

  .main-btn:hover {
    background: rgba(179,179,179,.42);
    color: #fff !important;
  }

  .center {
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
  }

  .centerCol {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }

  .color1 {
    margin-top: 20px;
    font-weight: 200;
  }
</style>
