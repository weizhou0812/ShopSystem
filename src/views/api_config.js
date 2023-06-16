import axios from "axios";
import {getToken} from "./auth.service"
import { ElMessage, ElMessageBox } from 'element-plus'

axios.defaults.baseURL="http://localhost:8080/api"
axios.defaults.headers['X-Request-With']="XMLHttpRequest"
axios.defaults.headers.post['Content-Type']='application/json'

//拦截器
axios.interceptors.request.use(options=>{
    //添加token
    const jwtToken=getToken()
    if(jwtToken){
        options.headers.Authorization=`Bearer ${jwtToken}`
    }
    return options
})
//响应拦截
axios.interceptors.response.use(res=>{
    return res
},error=>{
    ElMessage({
        message:error.response.data.message,
        type:"error"
    })
    return error
})
export default axios;