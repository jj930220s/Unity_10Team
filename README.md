수정 진행중
﻿ <div align="center">

# Void Hunter

<br/> [<img src="https://img.shields.io/badge/프로젝트 기간-2025.03.27~2025.04.02-73abf0?style=flat&logo=&logoColor=white" />]()

---
</div> 

## 📝 프로젝트 소개

플레이어 조작이 필요한 반자동 3D 뱀서류 게임 프로젝트입니다.  
플레이어는 몬스터를 쓰러트리고 스스로를 강화하며 게임을 진행해야 합니다.
  
마우스 조준을 통한 기본공격과 드론의 특수능력으로 전투를 이어가야 합니다.

---

## 🎮 게임 기능 개요

| 기능 | 설명 |
|----|---|
| **이동 및 공격** | WASD 키로 이동하고, 마우스를 통해 조준과 공격을 할 수 있습니다. |
| **강화** | 게임 플레이로 얻은 자원을 이용해 플레이어를 강화할 수 있습니다. |
| **드론** | 전투에 사용할 드론을 선택할 수 있습니다. 드론은 스스로 움직이며 특수한 동작으로 플레이어를 돕습니다. |
| **레벨** | 전투 중 획득한 경험치로 플레이어를 강화할 수 있습니다. 이 강화 내역은 전투가 끝나면 사라집니다. |

---

## 📸 화면 구성
|메인 화면|
|:---:|
|<img src="https://github.com/Dalsi-0/Factory404/blob/main/Readme/Main.png?raw=true" width="700"/>|
|게임 시작 및 설정으로 이동할 수 있는 화면입니다.|

<br /><br />

|게임 플레이 장면|
|:---:|
|<img src="https://github.com/Dalsi-0/Factory404/blob/main/Readme/Play1.png?raw=true" width="700"/>|
|<img src="https://github.com/Dalsi-0/Factory404/blob/main/Readme/Play2.png?raw=true" width="700"/>|
|<img src="https://github.com/Dalsi-0/Factory404/blob/main/Readme/Play3.png?raw=true" width="700"/>|
|<img src="https://github.com/Dalsi-0/Factory404/blob/main/Readme/Play4.png?raw=true" width="700"/>|
|플레이어가 공장을 탐색하며 다양한 오브젝트와 상호작용하는 장면입니다.|  

<br /><br />

|퍼즐|
|:---:|
|<img src="https://github.com/Dalsi-0/Factory404/blob/main/Readme/Puzzle1.png?raw=true" width="700"/>|
|<img src="https://github.com/Dalsi-0/Factory404/blob/main/Readme/Puzzle2.png?raw=true" width="700"/>|
|각종 기계 장치와 단서를 활용해 퍼즐을 해결하는 장면입니다.|

<br /><br />

|인벤토리|
|:---:|
|<img src="https://github.com/Dalsi-0/Factory404/blob/main/Readme/Inven1.png?raw=true" width="700"/>|
|<img src="https://github.com/Dalsi-0/Factory404/blob/main/Readme/Inven2.png?raw=true" width="700"/>|
|플레이어가 수집한 아이템을 확인하고 사용할 수 있는 인벤토리 화면입니다. |
|아이템을 마우스로 드래그해 회전시키며 상세하게 살펴볼 수 있습니다.|

<br /><br />

|로딩화면|
|:---:|
|<img src="https://github.com/Dalsi-0/Factory404/blob/main/Readme/Loading.png?raw=true" width="700"/>|
|각 스테이지 진입 전 표시되는 로딩 화면입니다.|
|포스트 프로세싱 효과를 활용해 시각적 변화를 주었습니다.|

---

## 👥 팀원 및 역할

| 팀원 | 역할 | GitHub 링크 |
|---|---|---|
| **유재혁 (팀장)** | 드론 시스템, 오브젝트풀 | [링크](https://github.com/jj930220s?tab=repositories)  |
| **김기석** | UI, 스텟 강화 | [링크](https://github.com/Kim-giseok) |
| **이정구** | 적 추적 AI | [링크](https://github.com/JUNG99) |
| **강진규** | 몬스터 패턴 | [링크](https://github.com/daeng98) |
| **진연호** | 플레이어, 시네머신 | [링크](https://github.com/yhjin0704) |

---

## 🤝 협업 툴

**GitHub:** 코드 버전 관리 및 협업

**Notion:** 프로젝트 문서 정리 및 일정 관리

---

## 🛠 **개발 및 기술적 접근**

### 🔧 사용 기술 스택  

**개발 엔진:** Unity  
**프로그래밍 언어:** C#  
**버전 관리:** GitHub  
**라이브러리:** Unitask  

<br /><br />

### 🎬 카메라 연출 – Cinemachine  
게임 내 다양한 연출을 위해 Cinemachine을 활용했습니다.  
플레이어 사망, 클리어 장면 등에서 부드러운 카메라 이동과 동적인 화면 효과를 구현했습니다.  

<br /><br />


### 🗺 네비게이션 메시 – Unity NavMesh  
적 몬스터들이 플레이어을 자연스럽게 쫒아 올 수 있도록 Navmesh를 이용했습니다.  
플레이어를 추적하다가 공격 사거리에 들어오면 자연스럽게 플레이어를 공격하도록 구현했습니다.

| Surface 컴포넌트  |
|------|
|<img src="https://github.com/Dalsi-0/Factory404/blob/main/Readme/Nav1.png?raw=true" width="500"/>|

<br /><br />


### 🏃 무한맵  
플레이어 이동에 따라 주변에 바닥타일을 만들거나 없애 무한히 이동할 수 있는 맵을 구현했습니다.  
이로 인해 연산 부담 없이 넓은 맵을 이용할 수 있습니다.

|무한맵 구현 코드|
|:---:|
|<img src="https://github.com/Dalsi-0/Factory404/blob/main/Readme/Slope.png?raw=true" width="500"/>|

<br /><br />


### ⚡ Unitask 활용 
이 프로젝트에서는 Unitask를 활용하여 일정 시간마다 동작하는 드론을 구현하였습니다.

|     |
|------|
|<img src="https://github.com/Dalsi-0/Factory404/blob/main/Readme/UniRx.png?raw=true" width="500"/>|

<br /><br />

---

## 📹 플레이 영상

**[![유튜브](https://github.com/Dalsi-0/Factory404/blob/main/Readme/Main.png?raw=true)](https://youtu.be/8zq_MxN52No)** 

---

