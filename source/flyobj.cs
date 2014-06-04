public class FlyObject 
{
  public double T = 0.01
  public string name
  public double mass//Две "s" по-приколу написал
  public Vector2 velocity
  public GameObject[] others
  
  double fx (localX) {
    double a = 0;
    for (int i=0; i<others.Length; i++) {
      r = Vector2(others[i].transform.position.x-localX, 
                  others[i].transform.position.y-transform.position.y)
                  .magnitude;
      a += others[i].mass*(others[i].transform.position.x - transform.position.x)/Math.pow(r, 3);
    }
    return a;
  }
  
  double fy (localY) {
    double a = 0;
    for (int i=0; i<others.Length; i++) {
      r = Vector2(others[i].transform.position.y-localY, 
                  others[i].transform.position.x-transform.position.x)
                  .magnitude;
      a += others[i].mass*(others[i].transform.position.y - transform.position.y)/Math.pow(r, 3);
    }
    return a;
  }
  
  void CalcX () {
    double k1 = T*fx(transform.position.x);
    double q1 = T*velocity.x;
    
    double k2 = T*fx(transform.position.x + q1/2);
    double q2 = T*(velocity.x + k1/2);
    
    double k3 = T*fx(transform.position.x + q2/2);
    double q3 = T*(velocity.x + k2/2);
    
    double k4 = T*fx(transform.position.x + q3);
    double q4 = T*(velocity.x + k3);
    
    velocity.x += Time.deltaTime*(k1 + 2 * k2 + 2 * k3 + k4) / 6;
  }
  
  void CalcY () {
    double k1 = T*fy(transform.position.y);
    double q1 = T*velocity.y;
    
    double k2 = T*fy(transform.position.y + q1/2);
    double q2 = T*(velocity.y + k1/2);
    
    double k3 = T*fy(transform.position.y + q2/2);
    double q3 = T*(velocity.y + k2/2);
    
    double k4 = T*fy(transform.position.y + q3);
    double q4 = T*(velocity.y + k3);
    
    velocity.y += Time.deltaTime*(k1 + 2 * k2 + 2 * k3 + k4) / 6;
  }
  
  void FixedUpdate () {
    calcX();
    calcY();
    transform.translate(Time.deltaTime*velocity.x, Time.deltaTime*velocity.y, 0);
  }
