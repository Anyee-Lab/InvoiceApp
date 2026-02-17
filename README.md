# InvoiceApp - 发票管理桌面应用

**发票管理桌面应用 | WinUI 3 + C# .NET 项目**  
一款现代化的 Windows 桌面发票工具，支持发票统一格式化、信息提取、智能识别等核心功能。采用 MVVM 架构，界面简洁友好、响应迅速，完美适配 Windows 11 Fluent Design，适合个人用户或小型企业高效处理发票报销、归档与管理。

## 主要功能

- **发票信息自动提取与智能识别**（支持 OCR 或手动输入，未来可集成更多 AI 模型）
- **发票管理**：创建、编辑、搜索、批量处理发票记录
- **MVVM 架构**：清晰分离视图、视图模型与服务层，便于维护和扩展
- **现代 UI**：WinUI 3 + Fluent Design，支持暗黑模式、圆角、亚克力效果

## 技术栈

- **UI 框架**：WinUI 3 (Windows App SDK)
- **语言**：C# .NET (推荐 .NET 8 或更高)
- **架构**：MVVM + CommunityToolkit.Mvvm / Microsoft.Toolkit.Mvvm

## 快速开始

### 1. 环境要求

- Windows 10/11（推荐 Windows 11）
- Visual Studio 2022（带 .NET desktop development 工作负载）
- Windows App SDK 扩展（Visual Studio Installer → 修改 → 单个组件 → Windows App SDK）

### 2. 克隆仓库

```bash
git clone https://github.com/Anyee-Lab/InvoiceApp.git
cd InvoiceApp
