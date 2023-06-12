const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true,
  devServer:{
    proxy:{
      "/api":{
        target:"https://localhost:7212/api/",//服务器请求地址
        secure:false,//HTTPS需要配置这个参数
        changeOrigin:true,//请求头host属性，false是发真实的本机过去，true把host设置成target
        pathRewrite:{
          "^/api":""
        }
      }
    }
  }
})
