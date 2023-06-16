import { createStore } from 'vuex'
//import uInfo from "./state/userinfo.state.js"
import authModule  from './state/userinfo.state'
export default createStore({
  modules: {
    //uInfo
    authModule
  }
})
