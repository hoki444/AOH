#include "HelloWorldScene.h"
#include "GameScene.h"
static std::string fontList[] =
{
#if (CC_TARGET_PLATFORM == CC_FLATFORM_IOS)
	"A Damn Mess",
	"Abberancy",
	"Abduction",
	"Paint Boy",
	"Schwarzwald Regular",
	"Scissor Cuts",
#else 
	"fonts/A Damn Mess.ttf",
	"fonts/Abberancy.ttf",
	"fonts/Abduction.ttf",
	"fonts/Paint Boy.ttf",
	"fonts/Schwarzwald Regular.ttf",
	"fonts/Scissor Cuts.ttf",
#endif
};
using namespace cocos2d;

CCScene* HelloWorld::scene()
{
    CCScene * scene = NULL;
    do 
    {
        // 'scene' is an autorelease object
        scene = CCScene::create();
        CC_BREAK_IF(! scene);

        // 'layer' is an autorelease object
        HelloWorld *layer = HelloWorld::create();
        CC_BREAK_IF(! layer);

        // add layer as a child to scene
        scene->addChild(layer);
    } while (0);

    // return the scene
    return scene;
}

// on "init" you need to initialize your instance
bool HelloWorld::init()
{
	if(! CCLayerColor::initWithColor(ccc4(100,255,100,255)))
	{
		return false;
	}

	CCMenuItemImage *pMenuItem1 = CCMenuItemImage::create("Images/play-normal.png",
		"Images/play-selected.png",this,menu_selector(HelloWorld::startGame));

	CCMenuItemImage *pMenuItem2 = CCMenuItemImage::create("Images/shop-normal.png",
		"Images/shop-selected.png",this,menu_selector(HelloWorld::goShop));

	CCMenuItemImage *pMenuItem3 = CCMenuItemImage::create("Images/exit-normal.png",
		"Images/exit-selected.png",this,menu_selector(HelloWorld::quitGame));

	CCSize size = CCDirector::sharedDirector()->getWinSize();

	CCLabelBMFont *pLabel = CCLabelBMFont::create("Arena Of Heroes","fonts/futura-48.fnt");
	pLabel->setPosition(ccp(size.width/2,250));
	this->addChild(pLabel);
	CCSprite* lMan= CCSprite::create("images/leftman.png");
	lMan->setScale(0.5);
	lMan->setPosition(ccp(80,100));
	this->addChild(lMan);
	CCSprite* rMan= CCSprite::create("images/rightman.png");
	rMan->setPosition(ccp(700,180));
	lMan->addChild(rMan);

	CCMenu* pMenu = CCMenu::create(pMenuItem1,pMenuItem2,pMenuItem3,NULL);
	pMenu->setPositionY(120);
	pMenu->alignItemsVertically();
	this->addChild(pMenu);

	return true;
}
void HelloWorld::startGame(CCObject* pSender)
{
	CCScene* pScene = GameScene::scene();
	CCDirector::sharedDirector()->pushScene(pScene);
}
void HelloWorld::goShop(CCObject* pSender)
{
	CCLog("doClick2함수에서 상점을 구현할 예정입니다.");
}
void HelloWorld::quitGame(CCObject* pSender)
{
	CCLog("doClick3함수에서 게임 종료를 구현할 예정입니다.");
}