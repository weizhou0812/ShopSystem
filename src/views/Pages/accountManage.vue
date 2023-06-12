<template>
    <div>
        <el-breadcrumb :separator-icon="ArrowRight">
            <el-breadcrumb-item :to="{ path: '/' }">首页</el-breadcrumb-item>
            <el-breadcrumb-item>账号管理</el-breadcrumb-item>
            <el-breadcrumb-item>账号列表</el-breadcrumb-item>
        </el-breadcrumb>
    </div>
    <div class="connet">
        <el-input placeholder="请输入关键字" class="input-with-select"
            style="width: 200px;margin-right: 15px;margin-left: -1190px;">
            <template #append>
                <el-button @click="handlesearch"><el-icon>
                        <Search />
                    </el-icon></el-button>
            </template>
        </el-input>
        <el-button type="primary" @click="handleadd">添加用户</el-button>
        <el-button type="primary" @click="handlereflesh">刷新数据</el-button>
    </div>
    <div class="userlist">
        <!-- Table表格 
        data：要展示的数组数据
        prop：每条数据对应的属性
        lable：类标题
        scope.row：相当于一条数据
        -->
        <el-table :data="list" border style="width: 100%">
            <el-table-column prop="uId" label="用户ID" />
            <el-table-column prop="uName" label="账号" />
            <el-table-column prop="uPassword" label="密码" />
            <el-table-column prop="uIdentity" label="身份" />
            <el-table-column prop="isOnJob" label="在职状态">
                <template v-slot="scoped">
                    <el-switch v-model="scoped.row.isOnJob"></el-switch>
                </template>
            </el-table-column>
            <el-table-column label="操作">
                <template #default="scope">
                    <el-button type="primary" @click="handleedit(scope.row)"><el-icon>
                            <Edit />
                        </el-icon></el-button>
                    <el-button type="danger" @click="handledelete"><el-icon>
                            <Delete />
                        </el-icon></el-button>
                </template>
            </el-table-column>
        </el-table>
        <el-pagination background style="margin-top: 10px;" layout=" prev, pager, next, jumper, sizes,->, total"
            v-model:current-page="paging.pagenum" v-model:page-size="paging.pagesize" :page-sizes="[5, 10, 20, 50]"
            :total="total" @size-change="handleSizeChange" @current-change="handleCurrentChange" />
    </div>
    <el-dialog v-model="dialogFormVisible" :title="dialogTitle" :tableRow="tableRow" width="30%">
        <el-form ref="userForm" :model="addUserData" :rules="rules">
            <el-form-item label="用户ID" prop="uId">
                <el-input v-model="addUserData.uId" placeholder="请输入用户ID" />
            </el-form-item>
            <el-form-item label="用户账号" prop="uName">
                <el-input v-model="addUserData.uName" placeholder="请输入用户账号" />
            </el-form-item>
            <el-form-item label="用户密码" prop="uPassword">
                <el-input v-model="addUserData.uPassword" type="password" placeholder="请输入用户密码" />
            </el-form-item>
            <el-form-item label="用户身份" prop="uIdentity">
                <el-input v-model="addUserData.uIdentity" placeholder="请输入用户身份" />
            </el-form-item>
            <el-form-item label="在职状态" prop="isOnJob">
                <el-switch v-model="addUserData.isOnJob" />
            </el-form-item>
        </el-form>
        <template #footer>
            <span class="dialog-footer">
                <el-button type="primary" @click="addUser(userForm)">确定</el-button>
                <el-button @click="dialogFormVisible = false">取消</el-button>
            </span>
        </template>
    </el-dialog>
    <!-- <UserDialog ref="dialogdisplay"></UserDialog> -->
</template>

<script>
import qs from 'qs'
import { ArrowRight } from '@element-plus/icons-vue'
import { toRefs, reactive, onMounted, ref } from 'vue'
import axios from '../api_config'
import { ElMessage } from 'element-plus';
// import UserDialog from '@/components/UserDialog.vue'
export default {
    name: "accountManage",
    setup() {
        const tableData = reactive(
            {
                list: [],
                dialogFormVisible: false,
                paging: {
                    query: "",
                    pagesize: 5,
                    pagenum: 1
                },
                addUserData: {
                    uId: "",
                    uName: "",
                    uPassword: "",
                    uIdentity: "",
                    isOnJob: false
                },
                rules: {
                    uId: [
                        { required: true, message: "此项不得为空", trigger: "blur" }
                    ],
                    uName: [
                        { required: true, message: "此项不得为空", trigger: "blur" }
                    ],
                    uPassword: [
                        { required: true, message: "此项不得为空", trigger: "blur" }
                    ],
                    uIdentity: [
                        { required: true, message: "此项不得为空", trigger: "blur" }
                    ],
                },
                total: "",
                dialogTitle: "",
            }

        );
        const getAlllist = () => {
            axios.get("/Users/getlistbypage?pageNum=" + tableData.paging.pagenum + "&&pageSize=" + tableData.paging.pagesize,)
                .then((res) => {
                    tableData.list = res.data;
                    console.log(res.data);
                });
            axios.get("/Users/Count")
                .then((res) => {
                    tableData.total = res.data
                    console.log(res.data)
                });
        };
        // onMounted(() => {
        //     getAlllist();
        // });
        getAlllist();

        const handlesearch = () => {

        };
        const handleadd = () => {
            tableData.dialogFormVisible = true
            tableData.dialogTitle = "添加账号"
        };
        const handlereflesh = () => {
            getAlllist();
        };
        const handleedit = (row) => {
            console.log(row)
            tableData.dialogFormVisible = true
            tableData.dialogTitle = "修改账号"
            tableData.addUserData = row
        };
        const handledelete = () => {
        };
        const addUser = (form) => {
            if (tableData.dialogTitle === "添加账号") {
                //validate:	对整个表单的内容进行验证。
                form.validate(res => {
                    if (!res) {
                        return
                    }
                    axios.post("/Users", tableData.addUserData).then(() => {
                        ElMessage.success("添加成功")
                        tableData.dialogFormVisible = false
                        tableData.addUserData = {
                            uId: "",
                            uName: "",
                            uPassword: "",
                            uIdentity: "",
                            isOnJob: false
                        }
                    })
                })
            }else{
                form.validate(res => {
                    if (!res) {
                        return
                    }
                    axios.put("/Users", tableData.addUserData).then(() => {
                        ElMessage.success("修改成功")
                        tableData.dialogFormVisible = false
                        tableData.addUserData = {
                            uId: "",
                            uName: "",
                            uPassword: "",
                            uIdentity: "",
                            isOnJob: false
                        }
                    })
                })
            }
        }
        const handleSizeChange = (val) => {
            tableData.paging.pagesize = val
            console.log(val)
            getAlllist();
        }
        const handleCurrentChange = (val) => {
            tableData.paging.pagenum = val
            console.log(val)
            getAlllist();
        }
        const userForm = ref()
        return {
            ...toRefs(tableData),
            handlesearch,
            handleadd,
            handlereflesh,
            handleedit,
            handledelete,
            addUser,
            userForm,
            getAlllist,
            handleSizeChange,
            handleCurrentChange
        };
    },
}
</script>
<style>
.connet {
    box-sizing: border-box;
    display: block;
    width: 100%;
    padding: 20px;
    background: #ffff;
}
</style>