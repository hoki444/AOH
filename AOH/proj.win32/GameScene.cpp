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

CCScene* GameScene::scene()//여기 바꾸기
{
	CCScene * scene = CCScene::create();//여기 바꾸기
    do 
    {
        // 'scene' is an autorelease object
        scene = CCScene::create();
        CC_BREAK_IF(! scene);

        // 'layer' is an autorelease object
        GameScene *layer = GameScene::create();//여기 바꾸기
        CC_BREAK_IF(! layer);

        // add layer as a child to scene
        scene->addChild(layer);
    } while (0);

    // return the scene
    return scene;
}

// on "init" you need to initialize your instance
bool GameScene::init()
{
	if(! CCLayerColor::initWithColor(ccc4(0,0,0,255)))
	{
		return false;
	}

	CCSize size = CCDirector::sharedDirector()->getWinSize();

	CCLabelBMFont *pLabel = CCLabelBMFont::create("Arena Of Heroes","fonts/futura-48.fnt");
	pLabel->setPosition(ccp(size.width/2,250));
	this->addChild(pLabel);
	CCSprite* field= CCSprite::create("images/field.png");
	field->setPosition(ccp(222,140));
	field->setScaleX(0.5);
	field->setScaleY(0.7);
	this->addChild(field);

	return true;
}