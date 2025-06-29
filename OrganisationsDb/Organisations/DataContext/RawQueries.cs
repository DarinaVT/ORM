using Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataContext;

public class RawQueries
{
    public List<Between1990And2018> RawQuery()
    {
        var results = new List<Between1990And2018>();
        using (var connection = new SqlConnection(GlobalParams.ConnectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(@"
            SELECT COUNT(o.Name) AS Amount, i.Name AS Industry
            FROM organisations o
            JOIN industries i ON i.Id = o.IndustryId
            JOIN years y ON y.Id = o.YearId
            WHERE y.Founded BETWEEN @minYear AND @maxYear
            GROUP BY i.Name", connection))
            {
                command.Parameters.AddWithValue("@minYear", 1990);
                command.Parameters.AddWithValue("@maxYear", 2018);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new Between1990And2018
                        {
                            Count = reader.GetInt32(0),
                            Industry = reader.GetString(1)
                        });
                    }
                }
            }
        }
        return results;
    }

}
public class Between1990And2018
{
    public int Count { get; set; }
    public string Industry { get; set; }
    public override string ToString()
    {
        return $"Count = {Count}, Industry = {Industry}";
    }
}