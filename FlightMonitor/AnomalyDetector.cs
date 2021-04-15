using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

public class Line
{
	public float a, b;
    public Line() { a = 0; b = 0; }
    public Line(float a, float b) { this.a = a; this.b = b; }
    public float f(float x) {
		return a* x+b;
	}
};

public class Point
{
    public float x, y;
    public Point(float x, float y){ this.x = x; this.y = y; }
    public Point(double x, double y) { this.x = (float)x; this.y = (float)y; }
    public Point(){ x = 0; y = 0; }
    public void setCords(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
};

public class Circle
{
    public Point center;
    public float radius;
    public Circle(Point c, float r){ center = c; r = radius; }
};
public class AnomalyReport
{
    public string description;
    public int timeStep;
    public float val1, val2;
    public AnomalyReport(string description, int timeStep, float val1, float val2)
    {
        this.description = description;
        this.timeStep = timeStep;
        this.val1 = val1;
        this.val2 = val2;
    }
};
public class correlatedFeatures
{
    public string feature1, feature2;  // names of the correlated features
    public float corrlation;
    public Line lin_reg;
    public float threshold;
    public Point center;
    public Annotation annotation;
    public Annotation GetAnnotation()
    {
        return annotation;
    }
};
public class Anomaly_Detection_Util
{
    static public float AVG(List<float> clm)
    {
        return clm.Sum() / clm.Count;
    }
    static public float COAVG(List<float> c1, List<float> c2)
    {
        float coavg = 0;
        for (int i = 0; i < c1.Count; i++)
        {
            coavg += c1[i] * c2[i];
        }
        coavg /= c1.Count;
        return coavg;
    }
    static public float VAR(List<float> clm)
    {
        float var = 0;
        float avg = AVG(clm);
        for (int i = 0; i < clm.Count; i++)
        {
            var += (clm[i] - avg) * (clm[i] - avg);
        }
        var /= clm.Count;
        return var;
    }
    static public float COV(List<float> c1, List<float> c2)
    {
        float cov = COAVG(c1, c2) - AVG(c1) * AVG(c2);
        return cov;
    }
    static public double Pearson(List<float> c1, List<float> c2)
    {
        double p = COV(c1, c2) / (Math.Sqrt(VAR(c1)) * Math.Sqrt(VAR(c2)));
        return p;
    }

    static public Line LinReg(List<float> c1, List<float> c2)
    {
        float a = COV(c1, c2) / VAR(c1);
        float b = AVG(c2) - a * AVG(c1);
        Line l = new Line(a, b);
        return l;
    }
    static public float dev( Point p, Line l){
        float x = p.y - l.f(p.x);
	    if(x<0)
            x*=-1;
	    return x;
    }
}
public class Timeseries
{
    private List<KeyValuePair<string, List<float>>> table;
    public int NumOfColumns => table.Count;
    public int NumOfRows => table[0].Value.Count;
    public Timeseries(List<string> columnNames)
    {
        table = new List<KeyValuePair<string, List<float>>>();
        foreach (string s in columnNames)
        {
            table.Add(new KeyValuePair<string, List<float>>(s, new List<float>()));
        }
    }
    public List<string> GetColumnNames()
    {
        List<string> columns = new List<string>();
        foreach (var curr in table)
        {
            columns.Add(curr.Key);
        }
        return columns;
    }
    public void AddRow(string line)
    {
        string[] sVals = line.Split(',');
        for (int i = 0; i < NumOfColumns; i++)
        {
            {
                table[i].Value.Add(float.Parse(sVals[i]));
            }
        }
    }
    public float FindValue(string name, int row)
    {
        return GetColumn(name)[row < NumOfRows ? row : NumOfRows - 1];
    }
    public List<float> GetColumn(string name)
    {
        List<float> l = table.Find(x => x.Key.Equals(name)).Value;
        if (l != null)
        {
            return l;
        }
        else
        {
            return new List<float>();
        }
    }
    public List<float> GetColumn(int index)
    {
        return table[index].Value;
    }

    public string GetColumnName(int index)
    {
        return table[index].Key;
    }
    public List<float> GetRow(int i)
    {
        List<float> l = new List<float>();
        foreach (var kvp in table)
        {
            l.Add(kvp.Value[i]);
        }
        return l;
    }

    static public List<Point> CombineColumns(List<float> c1, List<float> c2)
    {
        List<Point> l = new List<Point>();
        for (int i = 0; i < c1.Count; i++)
        {
            l.Add(new Point(c1[i], c2[i]));
        }
        return l;
    }
}

public class AnomalyDetector
{
    public List<correlatedFeatures> cf;
    public float linThreshold;
    public AnomalyDetector()
    {
        this.linThreshold = (float)0.8;
        cf = new List<correlatedFeatures>();
    }

    public void SetThreshold(float n)
    {
        linThreshold = n;
        cf = new List<correlatedFeatures>();
    }
    public List<correlatedFeatures> GetNormalModel()
    {
        return cf;
    }
    public virtual void addCorrelation(Timeseries ts, string feat1, string feat2, float pearson)
    {
        if (pearson > linThreshold)
        {
            correlatedFeatures correlation = new correlatedFeatures();
            correlation.feature1 = feat1;
            correlation.feature2 = feat2;
            correlation.corrlation = pearson;
            var Column1 = ts.GetColumn(feat1);
            var Column2 = ts.GetColumn(feat2);
            correlation.lin_reg = Anomaly_Detection_Util.LinReg(Column1, Column2);
            correlation.threshold = getMaxDev(Timeseries.CombineColumns(Column1, Column2), correlation.lin_reg);
            LineAnnotation regression = new LineAnnotation();
            Line linear_regression = correlation.lin_reg;
            regression.Slope = linear_regression.a;
            regression.Intercept = linear_regression.b;
            regression.Color = Colors.Blue;
            correlation.annotation = regression;
            cf.Add(correlation);
        }
    }
    public void LearnNormal(Timeseries ts)
    {
        for (int i = 0; i < ts.NumOfColumns - 1; i++)
        {
            List<float> Column1 = ts.GetColumn(i);
            float max = 0;
            int maxIndex = -1;
            for (int j = i + 1; j < ts.NumOfColumns; j++)
            {
                List<float> Column2 = ts.GetColumn(j);
                float correlativity =(float)Math.Abs(Anomaly_Detection_Util.Pearson(Column1, Column2));
                if (correlativity > max)
                {
                    max = correlativity;
                    maxIndex = j;
                }
            }
            if (maxIndex >= 0)
            {
                addCorrelation(ts, ts.GetColumnName(i), ts.GetColumnName(maxIndex), max);
            }
        }
    }
    public float getMaxDev(List<Point> points,Line lin_reg) {

        float maxDev = 0;
	    for (int i = 0; i<points.Count; i++)
	    {
		    float currDev =Anomaly_Detection_Util.dev(points[i], lin_reg);
		    if (currDev > maxDev)
		    {
			    maxDev = currDev;
		    }
	    }
	    return maxDev;
    }

    public virtual bool isAnomaly(Timeseries ts, correlatedFeatures cf,int timeStep) {

        List<float> Column1 = ts.GetColumn(cf.feature1);
        List<float> Column2 = ts.GetColumn(cf.feature2);
        Point currPoint = new Point(Column1[timeStep], Column2[timeStep]);
        float currDev = Anomaly_Detection_Util.dev(currPoint, cf.lin_reg);
        //return (Math.Abs(y - c.getLinearRegression().f(x)) > c.getThreshold());
        return (currDev > cf.threshold*1.1);
    }

    public List<AnomalyReport> detect(Timeseries ts){
	    List<AnomalyReport> anomalies = new List<AnomalyReport>();
        int numOfRows = ts.NumOfRows;
        int cfLen = cf.Count;
	    for (int cfIndex = 0; cfIndex<cfLen; cfIndex++)
	    {
		    for (int timeStep = 0; timeStep<numOfRows; timeStep++)
		    {
			    if (isAnomaly(ts, cf[cfIndex], timeStep))
			    {
                    //cout << "Anomaly found at " << timeStep + 1 << ": " << currDev << " crossed " << cf[cfIndex].threshold << std::endl;
                    AnomalyReport report = new AnomalyReport(cf[cfIndex].feature1 + "-" + cf[cfIndex].feature2, timeStep,ts.GetColumn(cf[cfIndex].feature1)[timeStep], ts.GetColumn(cf[cfIndex].feature2)[timeStep]);
                    anomalies.Add(report);
			    }
		    }
	    }
	    return anomalies;
    }
}

public class Circle_Util
{
    static public float distanceBetweenPoints(Point one, Point two) {
        return (float)Math.Sqrt((one.x - two.x) * (one.x - two.x) + (one.y - two.y) * (one.y - two.y));
    }

    static public bool pointInCircle(Point point, Circle circle)
    {
        return (distanceBetweenPoints(point, circle.center) < circle.radius + 1);
    }

    static public Circle findTrivialCircle(List<Point> points, int size)
    {
        size = points.Count;
        if (size == 2)
        {
            //cout << points[0]->x << "," << points[0]->y << " " << points[1]->x << "," << points[1]->y << endl;
            Point center = new Point((points[0].x +points[1].x)/ 2.0, (points[0].y + points[1].y) / 2.0);
            float radius = (float)(distanceBetweenPoints(points[0], points[1]) / 2.0);
            return new Circle(center, radius);
        }
        else if (size == 3)
        {
            Circle tmp = findTrivialCircle(points.GetRange(1, 2), 2);
            Circle tmp1 = findTrivialCircle(points.GetRange(0, 2), 2);
            if (pointInCircle(points[0], tmp) )
            {
                return tmp;
            }
            else if (pointInCircle(points[2], tmp1))
            {
                return tmp1;
            }
            else
            {
                Point p = points[1];
                points.RemoveAt(1);
                Circle ccc = findTrivialCircle(points, 2);
                if (pointInCircle(p, ccc))
                {
                    return ccc;
                }

                //Min circle must intersect with all 3 points.
                //Center of this circle is the circumcenter of the triangle of points[0],[1],[2]
                //Equations are all from https://math.wikia.org/wiki/Circumscribed_circle
                else
                {
                    points.Add(p);
                    float AX = points[0].x;
                    float AY = points[0].y;
                    float BX = points[1].x - AX;
                    float BY = points[1].y - AY;
                    float CX = points[2].x - AX;
                    float CY = points[2].y - AY;
                    float B = BX * BX + BY * BY;
                    float C = CX * CX + CY * CY;
                    float D = 2 * (BX * CY - BY * CX);
                    float a = distanceBetweenPoints(points[0], points[1]);
                    float b = distanceBetweenPoints(points[1], points[2]);
                    float c = distanceBetweenPoints(points[2], points[0]);
                    float centerX = (CY * (B) - BY * (C)) / D + AX;
                    float centerY = (BX * (C) - CX * (B)) / D + AY;
                    Point center = new Point(centerX, centerY);
                    float radius = (float)(a * b * c / Math.Sqrt((a * a + b * b + c * c) * (a * a + b * b + c * c) - 2 * (a * a * a * a + b * b * b * b + c * c * c * c)));
                    return (new Circle(center, radius));
                }
            }
        }
        else if (size == 0)
        {
            return new Circle(new Point(0, 0), 0);
        }
        return new Circle (points[0], 1);
    }
    static public Circle welzl(List<Point> p,  List<Point> r)
    {
        if(p.Count == 0|| r.Count == 3)
        {
            return Circle_Util.findTrivialCircle(r,3);
        }
        Point p0 = p[0];
        p.RemoveAt(0);
        Circle tmp = welzl(p, r);
        if (pointInCircle(p0,tmp))
        {
            return tmp;
        }
        r.Add(p0);
        return welzl(p, r);
    }
    static public Circle findMinCircle(List<Point>points, int size)
    {
        return welzl(points, new List<Point>());
    }
}

public class cAnomalyDetector : AnomalyDetector
{
    public float circThreshold;
    public cAnomalyDetector()
    {
        circThreshold = 0.6F;
    }

    public override bool isAnomaly(Timeseries ts, correlatedFeatures cf, int timeStep) {
        if (cf.corrlation > circThreshold)
        {
            var Column1 = ts.GetColumn(cf.feature1);
            var Column2 = ts.GetColumn(cf.feature2);
            Point currPoint = new Point(Column1[timeStep], Column2[timeStep]);
            //Point point = cf.center
            return (Circle_Util.distanceBetweenPoints(currPoint, cf.center) > cf.threshold * 1.1);
        }
        return false;
    }

    public override void addCorrelation(Timeseries ts, string feat1, string feat2, float pearson)
    {
        correlatedFeatures correlation = new correlatedFeatures();
        correlation.feature1 = feat1;
        correlation.feature2 = feat2;
        correlation.corrlation = pearson;
        var Column1 = ts.GetColumn(feat1);
        var Column2 = ts.GetColumn(feat2);
        var points = Timeseries.CombineColumns(Column1, Column2);
        Circle welzlCirc = Circle_Util.welzl(points, new List<Point>());
        correlation.center = welzlCirc.center;
        correlation.threshold = welzlCirc.radius;
        var a = new EllipseAnnotation();
        a.Fill = Colors.Transparent;
        a.StrokeThickness = 2;
        a.X = correlation.center.x;
        a.Y = correlation.center.y;
        a.Height = correlation.threshold * 2;
        a.Width = correlation.threshold * 2;
        correlation.annotation = a;
        correlation.annotation.Visibility = System.Windows.Visibility.Visible;
        
        cf.Add(correlation);
    }
}