    Public Property static Body* body As String
    Public Property void serialize(PrettyInputArchive& ar1, unsigned v) { BodyType type As String
    Public Property BodySize size As String
    Public Property ar1(type, size) As String
    Public Property body->setWeight(getDefaultWeight(size)) As String
    Public Property body->setSize(size) As String
    Public Property body->setMinPushSize(BodySize((int)size + 1)) As String
    Public Property switch (type) { case BodyType::Humanoid: body->setHumanoidBodyParts(getDefaultIntrinsicDamage(size)) As String
    Public Property body->setDeathSound(SoundId::HUMANOID_DEATH) As String
    Public Property body->setHumanoid(true) As String
    Public Property break As String
    Public Property case BodyType::HumanoidLike: body->setHumanoidBodyParts(getDefaultIntrinsicDamage(size)) As String
    Public Property body->setDeathSound(SoundId::BEAST_DEATH) As String
    Public Property break As String
    Public Property case BodyType::Bird: body->setBirdBodyParts(getDefaultIntrinsicDamage(size)) As String
    Public Property body->setDeathSound(SoundId::BEAST_DEATH) As String
    Public Property break As String
    Public Property case BodyType::FourLegged: body->setHorseBodyParts(getDefaultIntrinsicDamage(size)) As String
    Public Property body->setDeathSound(SoundId::BEAST_DEATH) As String
    Public Property break As String
    Public Property default: body->setDeathSound(SoundId::BEAST_DEATH) As String
    Public Property break As String