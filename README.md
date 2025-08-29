# Unity IdleAdventure

이 프로젝트는 Unity로 개발된 3D 방치형 게임입니다.

## 주요 스크립트
- **GameManager.cs**  
  GameManager.cs 게임의 전반적인 상태, 플레이어, 스테이지, 골드/경험치/레벨 등을 관리하는 싱글톤 스크립트입니다.

- **PlayerCondition.cs**  
  플레이어의 체력, 데미지, 그리고 회복을 담당합니다.

- **PlayerAttackController.cs / PlayerMovementController.cs**  
  플레이어의 행동과 이동을 제어하는 모듈형 컨트롤러입니다.

- **EnemyController.cs**  
  적의 인공지능, 체력, 공격, 그리고 처치 시 보상 로직을 처리합니다.
  
- **InventoryContainer.cs / ConsumableSlotContainer.cs**  
  아이템 인벤토리와 소모품 슬롯을 관리하며, UI 업데이트 및 아이템 사용 기능을 포함합니다.
  
- **ItemDataBase.cs / PortionItemData.cs**  
  모든 아이템의 기본 클래스와 물약 아이템의 예시 데이터입니다.

## 게임 플레이 테스트

- **소모품 추가**
  위쪽 화살표(Up Arrow) 키를 눌러 테스트 아이템을 소모품 슬롯에 추가할 수 있습니다.
  
- **골드 추가**
  아래쪽 화살표(Down Arrow) 키를 눌러 테스트용 골드를 얻을 수 있습니다.
