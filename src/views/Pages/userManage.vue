<template>
    <div>
        <el-breadcrumb :separator-icon="ArrowRight">
            <el-breadcrumb-item :to="{ path: '/' }">首页</el-breadcrumb-item>
            <el-breadcrumb-item>角色管理</el-breadcrumb-item>
            <el-breadcrumb-item>角色列表</el-breadcrumb-item>
        </el-breadcrumb>
    </div>
    <div class="connet">
        <el-input placeholder="请输入关键字" class="input-with-select" v-model="inputuid" clearable
            style="width: 200px;margin-right: 15px;margin-left: -1190px;">
            <template #append>
                <el-button @click="handlesearch"><el-icon>
                        <Search />
                    </el-icon></el-button>
            </template>
        </el-input>
        <el-button type="primary" @click="handleadd">添加信息</el-button>
        <el-button type="primary" @click="handlereflesh">刷新数据</el-button>
    </div>
    <div class="employeelist">
        <!-- Table表格 
        data：要展示的数组数据
        prop：每条数据对应的属性
        lable：类标题
        scope.row：相当于一条数据
        -->
        <el-table :data="list" border style="width: 100%">
            <el-table-column prop="eId" label="工号" width="80px" />
            <el-table-column prop="eName" label="姓名" width="80px" />
            <el-table-column prop="eSex" label="性别" width="60px" />
            <el-table-column prop="eAge" label="年龄" width="60px" />
            <el-table-column prop="eBirth" label="出生日期" width="100px" />
            <el-table-column prop="eEmail" label="邮箱" />
            <el-table-column prop="phoneNumber" label="电话号码" />
            <el-table-column prop="address" label="常驻地址" />
            <el-table-column prop="eIdentity" label="身份" width="100px" />
            <el-table-column prop="createdDate" label="创建时间" />
            <el-table-column prop="isOnJob" label="在职状态" width="100px">
                <template v-slot="scoped">
                    <el-switch v-model="scoped.row.isOnJob"></el-switch>
                </template>
            </el-table-column>
            <el-table-column label="操作" width="150px">
                <template #default="scope">
                    <el-button type="primary" @click="handleedit(scope.row)"><el-icon>
                            <Edit />
                        </el-icon></el-button>
                    <el-button type="danger" @click="handledelete(scope.row.eId)"><el-icon>
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
        <el-form ref="userForm" :model="addEmployeeData" :rules="rules">
            <el-form-item label="员工工号" prop="eId">
                <el-input v-model="addEmployeeData.eId" placeholder="请输入员工工号" />
            </el-form-item>
            <el-form-item label="员工姓名" prop="eName">
                <el-input v-model="addEmployeeData.eName" placeholder="请输入员工姓名" />
            </el-form-item>
            <el-form-item label="员工性别" prop="eSex">
                <el-select v-model="addEmployeeData.eSex" placeholder="请输入员工性别" style="width: 115px">
                    <el-option label="男" value="男" />
                    <el-option label="女" value="女" />
                </el-select>
            </el-form-item>
            <el-form-item label="员工年龄" prop="eAge">
                <el-input v-model="addEmployeeData.eAge" placeholder="请输入员工年龄" />
            </el-form-item>
            <el-form-item label="员工生日" prop="eBirth">
                <el-input v-model="addEmployeeData.eBirth" placeholder="请输入员工邮箱" />
            </el-form-item>
            <el-form-item label="邮箱" prop="eEmail">
                <el-input v-model="addEmployeeData.eEmail" placeholder="请输入员工邮箱" />
            </el-form-item>
            <el-form-item label="电话号码" prop="phoneNumber">
                <el-input v-model="addEmployeeData.phoneNumber" placeholder="请输入联系方式" />
            </el-form-item>
            <el-form-item label="常驻地址" prop="address">
                <el-input v-model="addEmployeeData.address" placeholder="请输入员工住址" />
            </el-form-item>
            <el-form-item label="身份" prop="eIdentity">
                <el-input v-model="addEmployeeData.eIdentity" placeholder="请输入员工身份" />
            </el-form-item>
            <el-form-item label="创建时间" prop="createdDate">
                <el-date-picker v-model="addEmployeeData.createdDate" type="datetime" placeholder="请填写创建时间" />
            </el-form-item>
            <el-form-item label="在职状态" prop="isOnJob">
                <el-switch v-model="addEmployeeData.isOnJob" />
            </el-form-item>
        </el-form>
        <template #footer>
            <span class="dialog-footer">
                <el-button type="primary" @click="addEmployee(userForm)">确定</el-button>
                <el-button @click="dialogFormVisible = false">取消</el-button>
            </span>
        </template>
    </el-dialog>
</template>

<script>
import { ArrowRight } from '@element-plus/icons-vue'
import { toRefs, reactive, onMounted, ref } from 'vue'
import axios from '../api_config'
import { ElMessage, ElMessageBox } from 'element-plus'
export default {
    name: "userManage",
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
                addEmployeeData: {
                    eId: "",
                    eName: "",
                    eSex: "",
                    eAge: ref(''),
                    eBirth: "",
                    eEmail: "",
                    phoneNumber: "",
                    address: "",
                    eIdentity: "",
                    createdDate: ref(''),
                    isOnJob: false
                },
                rules: {
                    eId: [
                        { required: true, message: "此项不得为空", trigger: "blur" }
                    ],
                    eName: [
                        { required: true, message: "此项不得为空", trigger: "blur" }
                    ],
                    eSex: [
                        { required: true, message: "此项不得为空", trigger: "blur" }
                    ],
                    eAge: [
                        { required: true, message: "此项不得为空", trigger: "blur" }
                    ],
                    eBirth: [
                        { required: true, message: "此项不得为空", trigger: "blur" }
                    ],
                    eEmail: [
                        { required: true, message: "此项不得为空", trigger: "blur" }
                    ],
                    phoneNumber: [
                        { required: true, message: "此项不得为空", trigger: "blur" }
                    ],
                    address: [
                        { required: true, message: "此项不得为空", trigger: "blur" }
                    ],
                    eIdentity: [
                        { required: true, message: "此项不得为空", trigger: "blur" }
                    ],
                },
                total: "",
                dialogTitle: "",
                inputuid: ""
            }

        );

        const getAlllist = () => {
            axios.get("/Employee/getlistbypage?pageNum=" + tableData.paging.pagenum + "&&pageSize=" + tableData.paging.pagesize,)
                .then((res) => {
                    tableData.list = res.data;
                    console.log(res.data);
                });
            axios.get("/Employee/Count")
                .then((res) => {
                    tableData.total = res.data
                    console.log(res.data)
                });
        };

        getAlllist();

        const handlesearch = () => {
            axios.get("/Employee/FuzzyQuery?eid=" + tableData.inputuid + "&&pageNum=" + tableData.paging.pagenum + "&&pageSize=" + tableData.paging.pagesize)
                .then((res) => {
                    tableData.list = res.data;
                });
            axios.get("/Employee/FQCount?eid=" + tableData.inputuid)
                .then((res) => {
                    tableData.total = res.data
                });
        };
        const handleadd = () => {
            tableData.dialogFormVisible = true
            tableData.dialogTitle = "添加信息"
        };
        const handlereflesh = () => {
            tableData.inputuid = '';
            getAlllist();
        };
        const handleedit = (row) => {
            console.log(row)
            tableData.dialogFormVisible = true
            tableData.dialogTitle = "修改信息"
            tableData.addEmployeeData = row
        };
        const handledelete = (eId) => {
            ElMessageBox.confirm(
                '你确定要删除此条信息吗？',
                '消息提示',
                {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning',
                }
            )
                .then(() => {
                    axios.delete("/Employee/byid?eid=" + eId)
                    ElMessage({
                        type: 'success',
                        message: '删除成功',
                    })
                    getAlllist();
                })
                .catch(() => {
                    ElMessage({
                        type: 'info',
                        message: '删除取消',
                    })
                })
        };
        const addEmployee = (form) => {
            if (tableData.dialogTitle === "添加信息") {
                //validate:	对整个表单的内容进行验证。
                form.validate(res => {
                    if (!res) {
                        return
                    }
                    axios.post("/Employee", tableData.addEmployeeData).then(() => {
                        ElMessage.success("添加成功")
                        tableData.dialogFormVisible = false
                        tableData.addEmployeeData = {
                            eId: "",
                            eName: "",
                            eSex: "",
                            eAge: ref(''),
                            eBirth: "",
                            eEmail: "",
                            phoneNumber: "",
                            address: "",
                            eIdentity: "",
                            createdDate: ref(''),
                            isOnJob: false
                        }
                        getAlllist();
                    })
                })
            } else {
                form.validate(res => {
                    if (!res) {
                        return
                    }
                    axios.put("/Employee", tableData.addEmployeeData).then(() => {
                        ElMessage.success("修改成功")
                        tableData.dialogFormVisible = false
                        tableData.addEmployeeData = {
                            eId: "",
                            eName: "",
                            eSex: "",
                            eAge: ref(''),
                            eBirth: "",
                            eEmail: "",
                            phoneNumber: "",
                            address: "",
                            eIdentity: "",
                            createdDate: ref(''),
                            isOnJob: false
                        }
                    })
                })
            }
        }
        const handleSizeChange = (val) => {
            tableData.paging.pagesize = val
            if (tableData.inputuid === "") { getAlllist(); }
            else {
                handlesearch();
            }
        }
        const handleCurrentChange = (val) => {
            tableData.paging.pagenum = val
            if (tableData.inputuid === "") { getAlllist(); }
            else {
                handlesearch();
            }
        }
        const userForm = ref()
        //const createdate = ref('')
        return {
            ...toRefs(tableData),
            handlesearch,
            handleadd,
            handlereflesh,
            handleedit,
            handledelete,
            addEmployee,
            userForm,
            getAlllist,
            handleSizeChange,
            handleCurrentChange,

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