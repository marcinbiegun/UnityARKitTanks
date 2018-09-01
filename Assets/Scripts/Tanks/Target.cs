namespace Tanks {
    public class Target {
        public float rotation;
        public float angle;
        public float power;

        public Target(float rot, float ang, float pow) {
            rotation = rot;
            angle = ang;
            power = pow;
        }

        // Overload + operator
        public static Target operator +(Target a, Target b) {
            return new Target(
                a.rotation + b.rotation,
                a.angle + b.angle,
                a.power + b.power
            );
        }

    }


}