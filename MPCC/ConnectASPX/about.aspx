<%@ Page Title="Connect | About" Language="C#" MasterPageFile="~/connect.master"
    AutoEventWireup="true" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="scripts/jquery.tweet.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script>
        $('.mainnav li').removeAttr('class');
        $('.mainnav li a[href$="about.aspx"]').parent().addClass('active');
        jQuery(function ($) {
            $(".tweet").tweet({
                username: "mpccgreenwood",
                avatar_size: 32,
                count: 5,
                loading_text: "loading tweets..."
            });
        });
    </script>
    <div class="main">
        <div class="main-inner">
            <div class="container">
                <div class="row">
                    <div class="span8">
                        <div class="widget">
                            <div class="widget-header">
                                <i class="icon-pushpin"></i>
                                <h3>
                                    About MPCC Connect</h3>
                            </div>
                            <div class="widget-content">
                                <h3>
                                    Welcome to Mount Pleasant Christian Church</h3>
                                <br />
                                <p>
                                    <a href="http://74.53.28.131/~mpcc/wp-content/uploads/2011/10/066philbeck.jpg">
                                        <img style="float: left; border: 8px #ccc solid; margin-right: 15px;" title="066philbeck"
                                            src="http://74.53.28.131/~mpcc/wp-content/uploads/2011/10/066philbeck-300x268.jpg"
                                            alt="" width="126" height="113" /></a> My hope is you will see how we are
                                    working to accomplish our mission of connecting people to Christ and igniting a
                                    passion for His purpose. I invite you to worship with us and discover the difference
                                    Jesus Christ can make in your life.<br />
                                    - Pastor Chris</p>
                                <p>
                                    &nbsp;<p>
                                <p>
                                    &nbsp;<p>
                                <p>
                                    Our <strong>Vision</strong> is to be a church that is locally focused and globally
                                    engaged with an undeniable influence for Christ.</p>
                                <p>
                                    Our <strong>Mission</strong> is to change the world for Christ one life, one family,
                                    one opportunity at a time.</p>
                                <p>
                                    Our <strong>Strategies</strong> (how we are accomplishing our vision)<br />
                                    Compelling Worship<br />
                                    Focused Discipleship<br />
                                    Serving Others (across the street and around the world)</p>
                                <p>
                                    At Mount Pleasant Christian Church we want to lead people to discover three things:
                                    <strong>a person, a pathway and a purpose</strong>. We want to lead people into
                                    a life-changing relationship with Jesus Christ (<strong>person</strong>). We want
                                    to help people discover a <strong>pathway</strong> for becoming a fully developing
                                    disciple of Christ. We want to equip people to live their lives for the <strong>purpose</strong>
                                    of serving Christ.</p>
                                <p>
                                    Our mission is simple; we want to change the world one life, one family, one opportunity
                                    at a time. Mount Pleasant is continually striving to achieve this goal. We provide
                                    a unique and dynamic worship atmosphere and once you attend Mount Pleasant, you’ll
                                    be able to access the many benefits of becoming a member.</p>
                                <p>
                                    Participating in a ministry at Mount Pleasant will expand your knowledge of Christ
                                    while engaging with others through fellowship. Determining which of your talents
                                    can be used to honor God is the key to connecting with one of these several ministries.
                                    Through this, you’ll be able to mature in your growth and love for Christ.</p>
                                <ul class="faq-list">
                                    <li><strong>CHILDREN</strong>
                                    <p>
                                        Come be a part of BIBLEOPOLIS!</p>
                                    <p>
                                        Our Children’s Ministry provides programming for your child (birth to fourth grade)
                                        during all worship services. With age-appropriate Bible lessons and worship songs,
                                        your child will learn and grow with God at an early age. With interactive lessons,
                                        games, and music, Bibleopolis is the place to be.</p>
                                    <p>
                                        <span>Go to Children&#8217;s ministry page&#8230;</span></p></li>
                                    <li><strong>STUDENTS</strong>
                                        <p>
                                            Our Student Ministry is designed to encourage all fifth–twelfth grade students to
                                            feel welcomed and accepted in a world that isn’t always that way. With worship,
                                            games, trips, and special events, there is nothing boring about going to church!</p>
                                        <p>
                                            <span><a title="Students" href="http://74.53.28.131/~mpcc/students/">Go to Student ministries’
                                                page…</a></span></p>
                                    </li>
                                    <li><strong>ADULTS</strong>
                                        <p>
                                            We understand that in a big congregation, it’s easy to feel lost or unnoticed. That’s
                                            why one of our main focuses here at Mount Pleasant is small groups. We have many
                                            groups in which adults can participate and feel connected.</p>
                                        <p>
                                            <span><a title="Adults" href="http://74.53.28.131/~mpcc/adults/">Go to Adults ministry’s
                                                page…</a></span></p>
                                    </li>
                                    <li><strong>SPORTS &amp; WELLNESS</strong>
                                        <p>
                                            With opportunities for the whole family, the Mount Pleasant Sports &amp; Recreation
                                            program provides a place for you and your family to stay in shape, get involved,
                                            and have good, clean fun! With a state-of-the-art facility the CLC offers a variety
                                            of classes and instruction for you to get fit.</p>
                                        <p>
                                            <span><a title="Sports &amp; Wellness" href="http://74.53.28.131/~mpcc/sports-wellness/">
                                                Go to Sports and Wellness page…</a></span></p>
                                    </li>
                                    <li><strong>WORSHIP &amp; ARTS</strong>
                                        <p>
                                            The Worship Ministry at Mount Pleasant Christian Church is more than just a weekend
                                            production. Our purpose is to provide dynamic worship experiences for the congregation
                                            while working together as a thriving community of artists. It is our goal to utilize
                                            music, technology, strategic planning and other creative elements to draw people
                                            to the Lord in corporate worship.</p>
                                        <p>
                                            <span><a title="Worship &amp; Arts" href="http://74.53.28.131/~mpcc/worship-arts/">Go
                                                Worship and Arts ministry page…</a></span></p>
                                    </li>
                                    <li><strong>MISSIONS</strong>
                                        <p>
                                            The world is your mission field. Here at Mount Pleasant Christian Church, we want
                                            to see God’s impact on His people. We provide a variety of avenues, both at home
                                            and abroad, to share your love of Jesus Christ with His people in need.</p>
                                        <p>
                                            <span><a title="Missions" href="http://74.53.28.131/~mpcc/missions/">Go to Missions
                                                page&#8230;</a></span></p>
                                    </li>
                                    <li><strong>CONGREGATIONAL CARE</strong>
                                        <p>
                                            We know that sometimes life is hard, and sometimes God’s people are hurting. We
                                            also know that God is always walking right beside us through it all. God allows
                                            us pain so that one day we may be a comfort to others who experience that same pain.
                                            Here at Mount Pleasant Christian Church, we want you to know that you are not alone.
                                            We provide a variety of care and support groups for those in need and those who
                                            are hurting.</p>
                                        <p>
                                            <span><a title="Congregational Care" href="http://74.53.28.131/~mpcc/congregational-care/">
                                                Go to Congregational Care ministries page...</a></span></p>
                                    </li>
                                    <li><strong>PRESCHOOL</strong>
                                        <p>
                                            Your child will find a loving, fun environment at Mount Pleasant Christian Church
                                            Preschool. MPCC Preschool is a place where he/she can explore the world; grow at
                                            his/her own level and experience community in a small group setting. Our Preschool
                                            area is a safe, welcoming space that encourages learning and fellowship along with
                                            preparing children for kindergarten or first grade.</p>
                                        <p>
                                            <span><a title="Preschool" href="http://74.53.28.131/~mpcc/preschool/">Go to Preschool
                                                page …</a></span></p>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="span4">
                        <div class="widget widget-box">
                            <div class="widget-header">
                                <i class="icon-twitter-sign"></i>
                                <h3>
                                    MPCC Tweets</h3>
                            </div>
                            <div class="widget-content tweet">
                              
                            </div>
                        </div>
                        <div class="widget widget-plain">
                            <div class="widget-content">
                                <a href="javascript:;" class="btn btn-large btn-warning btn-support-ask">Ask A Question</a>
                                <%-- <a href="javascript:;" class="btn btn-large btn-support-contact">Contact Support</a>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
