# 主程成长路线 · 阶段 01 · 第 3 周（Lab 周 · 归档）

> **归档日**：2026-07-11  
> **所属阶段**：[`阶段01-本月配置.md`](阶段01-本月配置.md)  
> **上一周**：[`W2-Exam周-归档.md`](W2-Exam周-归档.md)  
> **下一周**：[`W4-阶段项目-当前.md`](W4-阶段项目-当前.md)  
> **项目**：`Major4_week3_Text`

---

## 一、本周配置（回顾）

| 字段 | 值 |
|------|-----|
| **本阶段第几周** | 第 **3** 周 / 共 4 周 |
| 本周考纲单元 | **U06～U15**（动画 · 玩法 · AI · Editor） |
| 练习形态 | **5 × Lab** + **Major4_week3 Exam01 补练** |
| Lab 目录 | `Assets/Labs/Stage01_W3/` |
| Exam 补练 | `Exam01` 登录链 + 选服 + **MainScene UI 练习** |

---

## 二、Lab 交付快照（学员确认 · 2026-07-11）

| 天 | Lab | 单元 | P-L1 | 备注 |
|:--:|-----|------|:----:|------|
| D1 | Lab_Animation_01 | U06～U08 | ✅ | Animator Idle↔Run |
| D2 | Lab_Animation_02 | U09～U10 | ✅ | BlendTree 或 IK 口述 |
| D3 | Lab_Editor_01 | U15 | ✅ | MenuItem + EditorWindow |
| D4 | Lab_Gameplay_01 | U11～U13 | ✅ | 摇杆 + 状态切换 |
| D5 | Lab_AI_01 | U14 | ✅ | 巡逻→追击→攻击→脱战 |

**Lab 完成率**：**5 / 5** ✅

---

## 三、Major4_week3 · Exam01 补练（同周完成）

| 模块 | 状态 | 说明 |
|------|:----:|------|
| LoginScene + LoginController | ✅ | 密码 888888 · Console「登陆成功」 |
| ServerScene + ServerUI Prefab | ✅ | 第 4 步搭建 |
| ServerListController + ServerItemView | ✅ | 动态 10 条 · 单选 · 关闭 Panel |
| MainScene UI（练习） | ✅ | 简化版：Canvas + 导航 + 背包/技能 + MainUIController |
| 断网自检 | ✅ | 第 6 步清单 |

**工具脚本**：`MainSceneUIBeautify.cs` · `ServerSceneAutoSetup.cs` · `ServerListWireSetup.cs` · `Exam01DeliveryCheck.cs`

---

## 四、W3 末指标

| 指标 | 目标 | 实际 |
|------|------|------|
| Lab 完成 | ≥4/5 | **5/5** ✅ |
| K-Coverage | 58→65+ | **66*** |
| U06～U10 已掌握 | ≥3 个 →2 | **U06/U07/U08/U10 →2** |
| U15 Editor | ≥1 Lab | **Lab_Editor_01 ✅** |
| P-Score | ≥80 | **85*** |

---

## 五、W3 主要踩坑（已写入记忆库）

| ID | 摘要 |
|----|------|
| **W3-1** | MainScene 丢失 · 需自动创建 Canvas |
| **W3-2** | `bg_背包` 双栏图 · 格子须锚定右侧 |
| **W3-3** | UI 过度美化 · 考试用简化版即可 |
| **W3-4** | Login 跳转/密码与试卷不一致 |
| **W3-5** | ServerItemView Missing Script · GUID 恢复 |

详表：[`踩坑记忆库_lixiaotong.md`](../02-学员档案/踩坑记忆库_lixiaotong.md) **§二-O**

---

## 六、变更日志

| 日期 | 说明 |
|------|------|
| 2026-07-11 | W3 归档 · 学员确认 Lab 5/5 + Exam01 补练完成 · 开 W4 |
