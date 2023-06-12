import { createStore } from 'vuex'
import uInfo from "./state/userinfo.state.js"
export default createStore({
  modules: {
    uInfo
  }
})
