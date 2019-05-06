export default{
    randomId:function(){
        return (((1+Math.random())*0x10000)|0).toString(16).substring(1);
    },
    guid:function(){
        return (this.randomId()+this.randomId()+"-"+this.randomId()+"-"+this.randomId()+"-"+this.randomId()+"-"+this.randomId()+this.randomId()+this.randomId());
    }
}